/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) dotPDN LLC, Rick Brewster, Tom Jackson, and contributors.     //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See license-pdn.txt for full licensing and attribution details.             //
//                                                                             //
// Ported to Pinta by: Hanh Pham <hanh.pham@gmx.com>                           //
/////////////////////////////////////////////////////////////////////////////////

using System;
using Cairo;
using Pinta.Gui.Widgets;
using Pinta.Core;

namespace Pinta.Effects
{
	public class OutlineEffect : LocalHistogramEffect
	{
		
		private int intensity;

		public override string Icon {
			get { return "Menu.Effects.Stylize.Outline.png"; }
		}

		public override string Name {
			get { return Translations.GetString ("Outline"); }
		}

		public override bool IsConfigurable {
			get { return true; }
		}

		public override string EffectMenuCategory {
			get { return Translations.GetString ("Stylize"); }
		}

		public OutlineData Data { get { return (OutlineData)EffectData!; } } // NRT - Set in constructor

		public OutlineEffect ()
		{
			EffectData = new OutlineData ();
		}

		public override bool LaunchConfiguration ()
		{
			return EffectHelper.LaunchSimpleEffectDialog (this);
		}

		#region Algorithm Code Ported From PDN
		public unsafe override ColorBgra Apply (ColorBgra src, int area, int* hb, int* hg, int* hr, int* ha)
		{
			int minCount1 = area * (100 - this.intensity) / 200;
			int minCount2 = area * (100 + this.intensity) / 200;

			int bCount = 0;
			int b1 = 0;
			
			while (b1 < 255 && hb[b1] == 0)
				++b1;

			while (b1 < 255 && bCount < minCount1) {
				bCount += hb[b1];
				++b1;
			}

			int b2 = b1;
			while (b2 < 255 && bCount < minCount2) {
				bCount += hb[b2];
				++b2;
			}

			int gCount = g1Counter (hg, minCount1)[1];
			int g1 = g1Counter(hg,minCount1)[0];
			
			int g2 = g1;
			while (g2 < 255 && gCount < minCount2) {
				gCount += hg[g2];
				++g2;
			}

			int r1 = r1Counter(hr,minCount1)[0];
			int rCount = r1Counter (hr, minCount1)[1];

			int r2 = r1;
			while (r2 < 255 && rCount < minCount2) {
				rCount += hr[r2];
				++r2;
			}

			int a1 = a1Counter (hb, ha, minCount1)[0];
			int aCount = a1Counter (hb, ha, minCount1)[0];

			int a2 = a1;
			while (a2 < 255 && aCount < minCount2) {
				aCount += ha[a2];
				++a2;
			}

			return ColorBgra.FromBgra (
			    (byte)(255 - (b2 - b1)),
			    (byte)(255 - (g2 - g1)),
			    (byte)(255 - (r2 - r1)),
			    (byte)(a2));
		}

		public unsafe int[] a1Counter(int* hb,int* ha, int minCount1)
		{

			int[] array = new int[2];
			int aCount = 0;
			int a1 = 0;
			while (a1 < 255 && hb[a1] == 0)
				++a1;

			while (a1 < 255 && aCount < minCount1) {
				aCount += ha[a1];
				++a1;
			}
			array[0] = a1;
			array[1] = aCount;
			return array;

		}
		public unsafe int[] r1Counter(int* hr,int minCount1)
		{
			int[] array = new int[2];
			int rCount = 0;
			int r1 = 0;
			while (r1 < 255 && hr[r1] == 0)
				++r1;

			while (r1 < 255 && rCount < minCount1) {
				rCount += hr[r1];
				++r1;
			}
			array[0] = r1;
			array[1] = rCount;
			return array;

		}
		public unsafe int[] g1Counter(int* hg,int minCount1)
		{
			int[] array = new int[2];
			int g1=0;
			int gCount=0;
		    while (g1 < 255 && hg[g1] == 0)
				++g1;

			while (g1 < 255 && gCount < minCount1) {
				gCount += hg[g1];
				++g1;
			}
			array[0] = g1;
			array[1] = gCount;
			return array;
		}

		public unsafe override void Render (ImageSurface src, ImageSurface dest, Gdk.Rectangle[] rois)
		{
			int thickness;
			thickness = Data.Thickness;
			this.intensity = Data.Intensity;

			foreach (Gdk.Rectangle rect in rois)
				RenderRect (thickness, src, dest, rect);
		}

		#endregion

		public class OutlineData : EffectData
		{
			[Caption ("Thickness"), MinimumValue (1), MaximumValue (200)]
			public int Thickness = 3;

			[Caption ("Intensity"), MinimumValue (0), MaximumValue (100)]
			public int Intensity = 50;

			[Skip]
			public override bool IsDefault { get { return Thickness == 0; } }
		}
	}
}
