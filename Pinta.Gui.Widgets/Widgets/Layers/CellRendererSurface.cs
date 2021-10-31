// 
// CellRendererSurface.cs
//  
// Author:
//       Greg Lowe <greg@vis.net.nz>
// 
// Copyright (c) 2010 Greg Lowe
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
using Gtk;
using Cairo;
using Pinta.Core;

namespace Pinta.Gui.Widgets
{
	public class CellRendererSurface : CellRenderer
	{
		private ImageSurface surface;
		private Surface transparent;

		public CellRendererSurface (int width, int height)
		{
			// TODO: Respect cell padding (Xpad and Ypad).
			SetFixedSize (width, height);

			transparent = new Cairo.ImageSurface (Cairo.Format.ARGB32, width, height);
			Cairo.Color gray = new Cairo.Color (.75, .75, .75);

			// Create checkerboard background	
			int grid_width = 4;

			using (Cairo.Context g = new Cairo.Context (transparent)) {
				g.Color = new Cairo.Color (1, 1, 1);
				g.Paint ();

				for (int y = 0; y < height; y += grid_width)
					for (int x = 0; x < width; x += grid_width)
						if ((x / grid_width % 2) + (y / grid_width % 2) == 1)
							g.FillRectangle (new Cairo.Rectangle (x, y, grid_width, grid_width), gray);
			}	
		}

		[GLib.Property ("surface", "Get/Set Surface", "Set the cairo image surface to display a thumbnail of.")]
		public ImageSurface Surface {
			get { return surface; }
			set { surface = value; }
		}

		protected override void OnGetSize (Widget widget, ref Gdk.Rectangle cell_area, out int x_offset,
		                                   out int y_offset, out int width, out int height)
		{
			// TODO: Respect cell padding (Xpad and Ypad).
			x_offset = cell_area.Left;
			y_offset = cell_area.Top;
			width = cell_area.Width;
			height = cell_area.Height;
		}

		protected override void OnRender (Context cr, Widget widget, Gdk.Rectangle background_area,
		                                  Gdk.Rectangle cell_area, CellRendererState flags)
		{
			int x, y, width, height;

			OnGetSize (widget, ref background_area, out x, out y, out width, out height);

			cr.Save ();
			cr.Translate (x, y);
			RenderCell (cr, cell_area.Width, cell_area.Height);
			cr.Restore ();
		}

		private void RenderCell (Context g, int width, int height)
		{
			// Add some padding
			width -= 2;
			height -= 2;
			
			double scale;
			int draw_width = width;
			int draw_height = height;
			
			// The image is more constrained by height than width
			if ((double)width / (double)surface.Width >= (double)height / (double)surface.Height) {
				scale = (double)height / (double)(surface.Height);
				draw_width = (int)(surface.Width * height / surface.Height);
			} else {
				scale = (double)width / (double)(surface.Width);
				draw_height = (int)(surface.Height * width / surface.Width);
			}

			int offset_x = (int)((width - draw_width) / 2f);
			int offset_y = (int)((height - draw_height) / 2f);
			
			g.Save ();
			g.Rectangle (offset_x, offset_y, draw_width, draw_height);
			g.Clip ();

			g.SetSource (transparent);
			g.Paint ();

			g.Scale (scale, scale);
			g.SetSourceSurface (surface, (int)(offset_x / scale), (int)(offset_y / scale));
			g.Paint ();
			
			g.Restore ();

			// TODO: scale this box correctly to match layer aspect ratio
			g.Color = new Cairo.Color (0.5, 0.5, 0.5);
			g.Rectangle (offset_x + 0.5, offset_y + 0.5, draw_width, draw_height);
			g.LineWidth = 1;
			g.Stroke ();
		}
	}
}
