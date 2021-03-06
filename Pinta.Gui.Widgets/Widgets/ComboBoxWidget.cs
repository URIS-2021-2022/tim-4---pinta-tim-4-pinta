// 
// ComboBoxWidget.cs
//  
// Author:
//       Olivier Dufour <olivier.duff@gmail.com>
// 
// Copyright (c) 2010 Olivier Dufour
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
using System.Diagnostics.CodeAnalysis;
using Gtk;

namespace Pinta.Gui.Widgets
{
	public class ComboBoxWidget : FilledAreaBin
	{
		private Label label;
		private ComboBoxText combobox;

		public ComboBoxWidget (string[] entries)
		{
			Build ();

			foreach (var s in entries)
				combobox.AppendText (s);

			combobox.Changed += delegate {
				Changed?.Invoke (this, EventArgs.Empty);
			};
		}

		public string Label {
			get => label.Text;
			set => label.Text = value;
		}

		public int Active {
			get => combobox.Active;
			set => combobox.Active = value;
		}

		public string ActiveText => combobox.ActiveText;

		public event EventHandler? Changed;

		[MemberNotNull (nameof (label), nameof (combobox))]
		private void Build ()
		{
			// Section label + line
			var hbox1 = new HBox (false, 6);

			label = new Label ();
			hbox1.PackStart (label, false, false, 0);
			hbox1.PackStart (new HSeparator (), true, true, 0);

			// Combobox
			combobox = new ComboBoxText ();

			// Main layout
			var vbox = new VBox (false, 6) {
				hbox1,
				combobox
			};

			Add (vbox);

			vbox.ShowAll ();
		}
	}
}
