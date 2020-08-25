﻿using System.Windows.Media;

namespace JinHu.Visualization.Plotter2D.Common
{
  public class TransparentLimitsPalette : DecoratorPaletteBase
  {
    public TransparentLimitsPalette() { }

    public TransparentLimitsPalette(IPalette palette) : base(palette) { }

    public override Color GetColor(double t)
    {
      if (t < 0 || t > 1)
      {
        return Colors.Transparent;
      }

      return Palette.GetColor(t);
    }
  }
}
