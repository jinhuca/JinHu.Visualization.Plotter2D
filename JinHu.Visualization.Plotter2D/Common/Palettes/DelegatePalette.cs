using System;
using System.Windows.Media;

namespace JinHu.Visualization.Plotter2D.Common
{
  public sealed class DelegatePalette : PaletteBase
  {
    public DelegatePalette(Func<double, Color> _func) => func = _func ?? throw new ArgumentNullException(nameof(_func));
    private readonly Func<double, Color> func;
    public override Color GetColor(double t) => func(t);
  }
}
