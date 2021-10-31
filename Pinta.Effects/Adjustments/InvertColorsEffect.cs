﻿/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) dotPDN LLC, Rick Brewster, Tom Jackson, and contributors.     //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See license-pdn.txt for full licensing and attribution details.             //
//                                                                             //
// Ported to Pinta by: Jonathan Pobst <monkey@jpobst.com>                      //
/////////////////////////////////////////////////////////////////////////////////

using System;
using Cairo;
using Pinta.Core;

namespace Pinta.Effects
{
	//[System.ComponentModel.Composition.Export (typeof (BaseEffect))]
	public class InvertColorsEffect : BaseEffect
	{
		UnaryPixelOp op = new UnaryPixelOps.Invert ();

		public override string Icon {
			get { return "Menu.Adjustments.InvertColors.png"; }
		}

		public override string Text {
			get { return Mono.Unix.Catalog.GetString ("Invert Colors"); }
		}

		public override EffectAdjustment EffectOrAdjustment {
			get { return EffectAdjustment.Adjustment; }
		}
		
		public override Gdk.Key AdjustmentMenuKey {
			get { return Gdk.Key.I; }
		}
		
		public override void RenderEffect (ImageSurface src, ImageSurface dest, Gdk.Rectangle[] rois)
		{
			op.Apply (dest, src, rois);
		}
	}
}
