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
using System.Diagnostics.CodeAnalysis;

namespace Pinta.Effects
{
	public class AddNoiseEffect : BaseEffect
	{

		public override string Icon {
			get { return "Menu.Effects.Noise.AddNoise.png"; }
		}

		public override string Name {
			get { return Translations.GetString ("Add Noise"); }
		}

		public override bool IsConfigurable {
			get { return true; }
		}

		public override string EffectMenuCategory {
			get { return Translations.GetString ("Noise"); }
		}

		public NoiseData Data { get { return (NoiseData)EffectData!; } } // NRT - Set in constructor

		static AddNoiseEffect ()
		{
			InitLookup ();
		}

		public AddNoiseEffect ()
		{
			EffectData = new NoiseData ();
		}

		public override bool LaunchConfiguration ()
		{
			return EffectHelper.LaunchSimpleEffectDialog (this);
		}

		#region Algorithm Code Ported From PDN
		[ThreadStatic]
		private static Random threadRand ;
		private const int tableSize = 16384;
		private static int[] lookup;

		private static double NormalCurve (double x, double scale)
		{
			return scale * Math.Exp (-x * x / 2);
		}

		[MemberNotNull (nameof (lookup))]
		private static void InitLookup ()
		{
			double l = 5;
			double r = 10;
			double scale = 50;
			double sum = 0;

			while (r - l > 0.0000001) {
				sum = 0;
				scale = (l + r) * 0.5;

				for (int i = 0; i < tableSize; ++i) {
					sum += NormalCurve (16.0 * ((double)i - tableSize / 2) / tableSize, scale);

					if (sum > 1000000) {
						break;
					}
				}

				if (sum > tableSize) {
					r = scale;
				} else if (sum < tableSize) {
					l = scale;
				} else {
					break;
				}
			}

			lookup = new int[tableSize];
			sum = 0;
			int roundedSum = 0, lastRoundedSum;

			for (int i = 0; i < tableSize; ++i) {
				sum += NormalCurve (16.0 * ((double)i - tableSize / 2) / tableSize, scale);
				lastRoundedSum = roundedSum;
				roundedSum = (int)sum;

				for (int j = lastRoundedSum; j < roundedSum; ++j) {
					lookup[j] = (i - tableSize / 2) * 65536 / tableSize;
				}
			}
		}

		public unsafe override void Render (ImageSurface src, ImageSurface dst, Gdk.Rectangle[] rois)
		{

			int intensity;

			intensity = Data.Intensity;

			int colorSaturation = Data.ColorSaturation;
			double coverage = 0.01 * Data.Coverage;

			int dev = intensity * intensity / 4;
			int sat = colorSaturation * 4096 / 100;


			if (threadRand == null) {
				//threadRand = new Random (unchecked (System.Threading.Thread.CurrentThread.GetHashCode () ^
				//    unchecked ((int)DateTime.Now.Ticks)));
			}

			Random? localRand = threadRand;
			int[] localLookup = lookup;

			foreach (Gdk.Rectangle rect in rois) {
				for (int y = rect.Top; y <= rect.GetBottom (); ++y) {
					ColorBgra* srcPtr = src.GetPointAddressUnchecked (rect.Left, y);
					ColorBgra* dstPtr = dst.GetPointAddressUnchecked (rect.Left, y);

					for (int x = 0; x < rect.Width; ++x) {
						if (localRand?.NextDouble () > coverage) {
							*dstPtr = *srcPtr;
						} else {
							int r;
							int g;
							int b;
							int i;

							if(localRand == null) {

								throw new ArgumentNullException ("localRand was null in AddNoiseEffect, Render method");
								
							} else {
								
								r = localLookup[localRand!.Next (tableSize)];
							}
							
							g = localLookup[localRand.Next (tableSize)];
							b = localLookup[localRand.Next (tableSize)];

							i = (4899 * r + 9618 * g + 1867 * b) >> 14;


							r = i + (((r - i) * sat) >> 12);
							g = i + (((g - i) * sat) >> 12);
							b = i + (((b - i) * sat) >> 12);

							dstPtr->R = Utility.ClampToByte (srcPtr->R + ((r * dev + 32768) >> 16));
							dstPtr->G = Utility.ClampToByte (srcPtr->G + ((g * dev + 32768) >> 16));
							dstPtr->B = Utility.ClampToByte (srcPtr->B + ((b * dev + 32768) >> 16));
							dstPtr->A = srcPtr->A;
						}

						++srcPtr;
						++dstPtr;
					}
				}
			}
		}
		#endregion

		public class NoiseData : EffectData
		{
			[Caption ("Intensity"), MinimumValue (0), MaximumValue (100)]
			private int intensity = 64;
			public int Intensity {
				get { return intensity; }
				set { intensity = value; }
			}

			[Caption ("Color Saturation"), MinimumValue (0), MaximumValue (400)]
			public int ColorSaturation = 100;

			[Caption ("Coverage"), MinimumValue (0), DigitsValue (2), MaximumValue (100)]
			public double Coverage = 100.0;
		}
	}
}
