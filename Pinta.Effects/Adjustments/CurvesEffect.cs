/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) dotPDN LLC, Rick Brewster, Tom Jackson, and contributors.     //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See license-pdn.txt for full licensing and attribution details.             //
//                                                                             //
// Ported to Pinta by: Krzysztof Marecki <marecki.krzysztof@gmail.com>         //
/////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.ComponentModel;
using Cairo;
using Pinta.Core;

namespace Pinta.Effects
{
	public class CurvesEffect : BaseEffect
	{
		UnaryPixelOp? op = null;
		
		public override string Icon {
			get { return "Menu.Adjustments.Curves.png"; }
		}

		public override string Name {
			get { return Translations.GetString ("Curves"); }
		}

		public override bool IsConfigurable {
			get { return true; }
		}

		public override string AdjustmentMenuKey {
			get { return "M"; }
		}

		public CurvesData Data { get { return (CurvesData)EffectData!; } } // NRT - Set in constructor
		
		public CurvesEffect ()
		{
			EffectData = new CurvesData ();
		}
		
		public override bool LaunchConfiguration ()
		{
			using (var dialog = new CurvesDialog(Data))
			{
				dialog.Title = Name;
				dialog.Icon = PintaCore.Resources.GetIcon(Icon);

				int response = dialog.Run();

				return (response == (int)Gtk.ResponseType.Ok);
			}
		}
		
		public override void Render (ImageSurface src, ImageSurface dest, Gdk.Rectangle[] rois)
		{
			if (Data.ControlPoints == null)
				return;

			if (op == null)
				op = MakeUop ();
			
			op.Apply (dest, src, rois);
		}
		
		private UnaryPixelOp MakeUop()
        {
            UnaryPixelOp op1;
            byte[][] transferCurves;
            int entries;

            switch (Data.Mode) {
                case ColorTransferMode.Rgb:
                    UnaryPixelOps.ChannelCurve cc = new UnaryPixelOps.ChannelCurve();
                    transferCurves = new byte[][] { cc.CurveR, cc.CurveG, cc.CurveB };
                    entries = 256;
                    op1 = cc;
                    break;

                case ColorTransferMode.Luminosity:
                    UnaryPixelOps.LuminosityCurve lc = new UnaryPixelOps.LuminosityCurve();
                    transferCurves = new byte[][] { lc.Curve };
                    entries = 256;
                    op1 = lc;
                    break;

                default:
                    throw new InvalidEnumArgumentException();
            }

            
            int channels = transferCurves.Length;

            for (int channel = 0; channel < channels; channel++) {
                SortedList<int, int> channelControlPoints = Data.ControlPoints![channel]; // NRT - Code expects this to be not-null
                IList<int> xa = channelControlPoints.Keys;
                IList<int> ya = channelControlPoints.Values;
                SplineInterpolator interpolator = new SplineInterpolator();
                int length = channelControlPoints.Count;

                for (int i = 0; i < length; i++) {
                    interpolator.Add(xa[i], ya[i]);
                }

                for (int i = 0; i < entries; i++) {
                    transferCurves[channel][i] = Utility.ClampToByte(interpolator.Interpolate(i));
                }
            }

            return op;
        }
	}
	
	public class CurvesData : EffectData
	{
		public SortedList<int, int>[]? ControlPoints { get; set; }
		
		public ColorTransferMode Mode { get; set; }
		
		public override EffectData Clone ()
		{
//			
//				
			
			return new CurvesData () {
				Mode = Mode,
				ControlPoints = ControlPoints
			};
		}
	}
}
