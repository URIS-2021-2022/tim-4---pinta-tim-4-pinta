// 
// WorkspaceManager.cs
//  
// Author:
//       Jonathan Pobst <monkey@jpobst.com>
// 
// Copyright (c) 2010 Jonathan Pobst
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Linq;
using Cairo;
using System.Collections.Generic;
using Gtk;

namespace Pinta.Core
{
	public interface IWorkspaceService
	{
		Document ActiveDocument { get; }
		DocumentWorkspace ActiveWorkspace { get; }
		event EventHandler? ActiveDocumentChanged;
		Gdk.Rectangle ClampToImageSize (Gdk.Rectangle r);
		bool HasOpenDocuments { get; }
		event EventHandler? SelectionChanged;
		SelectionModeHandler SelectionHandler { get; }
	}

	public class WorkspaceManager : IWorkspaceService
	{
		private int active_document_index = -1;
		private int new_file_name = 1;
		
		public WorkspaceManager ()
		{
			OpenDocuments = new List<Document> ();
            SelectionHandler = new SelectionModeHandler ();
		}

		public int ActiveDocumentIndex {
			get {
				return active_document_index;
			}
		}
		
		public Document ActiveDocument {
			get {
				if (HasOpenDocuments)
					return OpenDocuments[active_document_index];
				
				throw new InvalidOperationException ("Tried to get WorkspaceManager.ActiveDocument when there are no open Documents.  Check HasOpenDocuments first.");
			}
		}

		public Document? ActiveDocumentOrDefault => HasOpenDocuments ? OpenDocuments[active_document_index] : null;

        public SelectionModeHandler SelectionHandler { get; private set; }

		public DocumentWorkspace ActiveWorkspace {
			get {
				if (HasOpenDocuments)
					return OpenDocuments[active_document_index].Workspace;

				throw new InvalidOperationException ("Tried to get WorkspaceManager.ActiveWorkspace when there are no open Documents.  Check HasOpenDocuments first.");
			}
		}

		public Gdk.Size ImageSize {
			get { return ActiveDocument.ImageSize; }
			set { ActiveDocument.ImageSize = value; }
		}

		public Gdk.Size CanvasSize {
			get { return ActiveWorkspace.CanvasSize; }
			set { ActiveWorkspace.CanvasSize = value; }
		}
		
		public PointD Offset {
			get { return ActiveWorkspace.Offset; }
		}
		
		public double Scale {
			get { return ActiveWorkspace.Scale; }
			set { ActiveWorkspace.Scale = value; }
		}
		
		public List<Document> OpenDocuments { get; private set; }
		public bool HasOpenDocuments { get { return OpenDocuments.Count > 0; } }
		
		public Document CreateAndActivateDocument (string? filename, Gdk.Size size)
		{
			Document doc = new Document (size);
			
			if (string.IsNullOrEmpty (filename))
				doc.Filename = string.Format (Translations.GetString ("Unsaved Image {0}"), new_file_name++);
			else
				doc.PathAndFileName = filename;
			
			OpenDocuments.Add (doc);
			OnDocumentCreated (new DocumentEventArgs (doc));

			SetActiveDocument (doc);
			
			return doc;
		}

		public void CloseActiveDocument ()
		{
			CloseDocument (ActiveDocument);
		}
		
		public void CloseDocument (Document document)
		{
			int index = OpenDocuments.IndexOf (document);
			OpenDocuments.Remove (document);
			
			if (index == active_document_index) {
				// If there's other documents open, switch to one of them
				if (HasOpenDocuments) {
					if (index > 0)
						SetActiveDocument (index - 1);
					else
						SetActiveDocument (index);
				} else {
					active_document_index = -1;
					OnActiveDocumentChanged (EventArgs.Empty);
				}
			}

			document.Close ();
			
			OnDocumentClosed (new DocumentEventArgs (document));
		}
		
		public void Invalidate ()
		{
			if (PintaCore.Workspace.HasOpenDocuments)
				ActiveWorkspace.Invalidate ();
		}
		
		public void Invalidate (Gdk.Rectangle rect)
		{
			ActiveWorkspace.Invalidate (rect);
		}

		public Document NewDocument (Gdk.Size imageSize, Color backgroundColor)
		{
			Document doc = CreateAndActivateDocument (null, imageSize);
			doc.Workspace.CanvasSize = imageSize;

			// Start with an empty white layer
			Layer background = doc.Layers.AddNewLayer (Translations.GetString ("Background"));

			if (backgroundColor.A != 0) {
				using (Cairo.Context g = new Cairo.Context (background.Surface)) {
					g.SetSourceColor (backgroundColor);
					g.Paint ();
				}
			}

			doc.Workspace.History.PushNewItem (new BaseHistoryItem (Resources.StandardIcons.DocumentNew, Translations.GetString ("New Image")));
			doc.Workspace.History.SetClean();

			// This ensures these are called after the window is done being created and sized.
			// Without it, we sometimes try to zoom when the window has a size of (0, 0).
			Gtk.Application.Invoke (delegate {
				PintaCore.Actions.View.ZoomToWindow.Activate ();
			});

			return doc;
		}

		/// <summary>
		/// Creates a new Document with a specified image as content.
		/// Primarily used for Paste Into New Image.
		/// </summary>
		public Document NewDocumentFromImage (Gdk.Pixbuf image)
		{
			var doc = NewDocument (new Gdk.Size (image.Width, image.Height), new Color (0, 0, 0, 0));

			using (var g = new Context (doc.Layers[0].Surface))
				g.DrawPixbuf (image, new Point (0, 0));

			// A normal document considers the "New Image" history to not be dirty, as it's just a
			// blank background. We put an image there, so we should try to save if the user closes it.
			doc.Workspace.History.SetDirty ();

			return doc;
		}

		// TODO: Standardize add to recent files
		public bool OpenFile (string file, Window? parent = null)
		{
			bool fileOpened = false;

			if (parent == null)
				parent = PintaCore.Chrome.MainWindow;

			try {
				// Open the image and add it to the layers
				IImageImporter? importer = PintaCore.System.ImageFormats.GetImporterByFile (file);
				if (importer == null)
					throw new FormatException( Translations.GetString ("Unsupported file format"));

				importer.Import (file, parent);

				PintaCore.Workspace.ActiveDocument.PathAndFileName = file;
				PintaCore.Workspace.ActiveWorkspace.History.PushNewItem (new BaseHistoryItem (Resources.StandardIcons.DocumentOpen, Translations.GetString ("Open Image")));
				PintaCore.Workspace.ActiveDocument.History.SetClean();
				PintaCore.Workspace.ActiveDocument.HasFile = true;

                // This ensures these are called after the window is done being created and sized.
                // Without it, we sometimes try to zoom when the window has a size of (0, 0).
                Gtk.Application.Invoke (delegate {
				    PintaCore.Actions.View.ZoomToWindow.Activate ();
				    PintaCore.Workspace.Invalidate ();
                });

				fileOpened = true;
			} catch (UnauthorizedAccessException) {
				ShowFilePermissionErrorDialog (parent, file);
			} catch (FormatException e) {
				ShowUnsupportedFormatDialog (parent, file, e.Message, e.ToString());
			} catch (Exception e) {
				ShowOpenFileErrorDialog (parent, file, e.Message, e.ToString ());
			}

			return fileOpened;
		}
		
		public void ResizeImage (int width, int height)
		{
			ActiveDocument.ResizeImage (width, height);
		}
		
		public void ResizeCanvas (int width, int height, Anchor anchor, CompoundHistoryItem? compoundAction)
		{
			ActiveDocument.ResizeCanvas (width, height, anchor, compoundAction);
		}

		/// <summary>
		/// Converts a point from the active documents
		/// window coordinates to canvas coordinates
		/// </summary>
		/// <param name='x'>
		/// The X coordinate of the window point
		/// </param>
		/// <param name='y'>
		/// The Y coordinate of the window point
		/// </param>
		public Cairo.PointD WindowPointToCanvas (double x, double y)
		{
			return ActiveWorkspace.WindowPointToCanvas (x, y);
		}

		/// <summary>
		/// Converts a point from the active documents
		/// canvas coordinates to window coordinates
		/// </summary>
		/// <param name='x'>
		/// The X coordinate of the canvas point
		/// </param>
		/// <param name='y'>
		/// The Y coordinate of the canvas point
		/// </param>
		public Cairo.PointD CanvasPointToWindow (double x, double y)
		{
			return ActiveWorkspace.CanvasPointToWindow (x, y);
		}

		public Gdk.Rectangle ClampToImageSize (Gdk.Rectangle r)
		{
			return ActiveDocument.ClampToImageSize (r);
		}

		public bool ImageFitsInWindow {
			get { return ActiveWorkspace.ImageFitsInWindow; }
		}
		
		internal void ResetTitle ()
		{
			if (HasOpenDocuments)
				PintaCore.Chrome.MainWindow.Title = string.Format ("{0}{1} - Pinta", ActiveDocument.Filename, ActiveDocument.IsDirty ? "*" : "");
			else
				PintaCore.Chrome.MainWindow.Title = "Pinta";
		}

		public void SetActiveDocument (int index)
		{
			if (index >= OpenDocuments.Count)
				throw new ArgumentOutOfRangeException ("Tried to WorkspaceManager.SetActiveDocument greater than OpenDocuments.");
			if (index < 0)
				throw new ArgumentOutOfRangeException ("Tried to WorkspaceManager.SetActiveDocument less that zero.");
			
			SetActiveDocument (OpenDocuments[index]);
		}
		
		public void SetActiveDocument (Document document)
		{
			PintaCore.Actions.Window.SetActiveDocument(document);
		}

		internal void SetActiveDocumentInternal (Document document)
		{
			// Work around a case where we closed a document but haven't updated
			// the active_document_index yet and it points to the closed document
			if (HasOpenDocuments && active_document_index != -1 && OpenDocuments.Count > active_document_index)
				PintaCore.Tools.Commit ();

			int index = OpenDocuments.IndexOf (document);
			active_document_index = index;

			OnActiveDocumentChanged (EventArgs.Empty);
		}
		
		#region Protected Methods
		protected void OnActiveDocumentChanged (EventArgs e)
		{
			if (ActiveDocumentChanged != null)
				ActiveDocumentChanged (this, EventArgs.Empty);

            OnSelectionChanged ();
				
			ResetTitle ();
		}

		protected internal void OnDocumentCreated (DocumentEventArgs e)
		{
            e.Document.SelectionChanged += (sender, args) => {
		        OnSelectionChanged ();
		    };

			if (DocumentCreated != null)
				DocumentCreated (this, e);
		}

		protected internal void OnDocumentOpened (DocumentEventArgs e)
		{
			if (DocumentOpened != null)
				DocumentOpened (this, e);
		}

		protected internal void OnDocumentClosed (DocumentEventArgs e)
		{
			if (DocumentClosed != null)
				DocumentClosed (this, e);
		}

		private void OnSelectionChanged ()
		{
            if (SelectionChanged != null)
                SelectionChanged.Invoke(this, EventArgs.Empty);
        }
#endregion

        private void ShowOpenFileErrorDialog (Window parent, string filename, string primaryText, string details)
		{
			string markup = "<span weight=\"bold\" size=\"larger\">{0}</span>\n\n{1}";
			string secondaryText = string.Format (Translations.GetString ("Could not open file: {0}"), filename);
			string message = string.Format (markup, primaryText, secondaryText);
			PintaCore.Chrome.ShowErrorDialog(parent, message, details);
		}

		private void ShowUnsupportedFormatDialog (Window parent, string filename, string primaryText, string details)
		{
			string markup = "<span weight=\"bold\" size=\"larger\">{0}</span>\n\n{1}";

			string secondaryText = Translations.GetString("Could not open file: {0}", filename);
			secondaryText += string.Format("\n\n{0}\n", Translations.GetString("Pinta supports the following file formats:"));
			var extensions = from format in PintaCore.System.ImageFormats.Formats
							 where format.Importer != null
							 from extension in format.Extensions
							 where char.IsLower(extension.FirstOrDefault())
							 orderby extension
							 select extension;

			secondaryText += String.Join(", ", extensions);

			string message = string.Format (markup, primaryText, secondaryText);
			PintaCore.Chrome.ShowUnsupportedFormatDialog(parent, message, details);
		}

		private static void ShowFilePermissionErrorDialog (Window parent, string filename)
		{
			string markup = "<span weight=\"bold\" size=\"larger\">{0}</span>\n\n{1}";
			string primary = Translations.GetString ("Failed to open image");
			// Translators: {0} is the name of a file that the user does not have permission to open.
			string secondary = Translations.GetString ("You do not have access to '{0}'.", filename);
			string message = string.Format (markup, primary, secondary);

			using var md = new MessageDialog (parent, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, message);
			md.Run ();
		}

		#region Public Events
		public event EventHandler? ActiveDocumentChanged;
		public event EventHandler<DocumentEventArgs>? DocumentCreated;
		public event EventHandler<DocumentEventArgs>? DocumentOpened;
		public event EventHandler<DocumentEventArgs>? DocumentClosed;
		public event EventHandler? SelectionChanged;
#endregion
		
	}
}
