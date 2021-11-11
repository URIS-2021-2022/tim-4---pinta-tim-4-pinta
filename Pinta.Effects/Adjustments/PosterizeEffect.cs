/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) dotPDN LLC, Rick Brewster, Tom Jackson, and contributors.     //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See license-pdn.txt for full licensing and attribution details.             //
//                                                                             //
// Ported to Pinta by: Krzysztof Marecki <marecki.krzysztof@gmail.com>         //
/////////////////////////////////////////////////////////////////////////////////

using System;
using Cairo;
using Pinta.Core;

namespace Pinta.Effects
{
	public class PosterizeEffect : BaseEffect
	{
		UnaryPixelOps.PosterizePixel? op = null;
		
		public override string Icon {
			get { return "Menu.Adjustments.Posterize.png"; }
		}

		public override string Name {
			get { return Translations.GetString ("Posterize"); }
		}

		public override bool IsConfigurable {
			get { return true; }
		}

		public override string AdjustmentMenuKey {
			get { return "P"; }
		}

		public PosterizeData Data { get { return (PosterizeData)EffectData!; } } // NRT - Set in constructor
		
		public PosterizeEffect ()
		{
			EffectData = new PosterizeData ();
		}

		public override bool LaunchConfiguration ()
		{
			using (var dialog = new PosterizeDialog())
			{
				dialog.Title = Name;
				dialog.Icon = PintaCore.Resources.GetIcon(Icon);
				dialog.EffectData = Data;

				int response = dialog.Run();
				return (response == (int)Gtk.ResponseType.Ok);
			}
		}

		public override void Render (ImageSurface src, ImageSurface dst, Gdk.Rectangle[] rois)
		{
			if (op == null)
				op = new UnaryPixelOps.PosterizePixel (Data.Red, Data.Green, Data.Blue);
				
			op.Apply (dst, src, rois);
		}		
	}
	
	public class PosterizeData : EffectData
	{
		private int red = 16;
		public int Red
		{
			get { return red; }
			set { red = value ; }
		}
    
		private int green = 16;
		public int Green
		{
			get { return green; }
			set { green = value ; }
		}

		private int blue = 16;
		public int Blue
		{
			get { return blue; }
			set { blue = value ; }
		}
	}
}
