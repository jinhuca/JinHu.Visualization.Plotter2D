using System;
using System.Windows.Media;

namespace JinHu.Visualization.Plotter2D.Common
{
  public class PowerPalette : DecoratorPaletteBase
  {
    public PowerPalette() { }

    public PowerPalette(IPalette palette) : base(palette) { }

    public override Color GetColor(double t)
    {
      // todo create a property for power base setting
      return base.GetColor(Math.Pow(t, 0.1));
    }
  }
}
