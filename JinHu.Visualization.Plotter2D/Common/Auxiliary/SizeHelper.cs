using System.Windows;

namespace JinHu.Visualization.Plotter2D.Common
{
  internal static class SizeHelper
  {
    public static Size CreateInfiniteSize()
    {
      return new Size(double.PositiveInfinity, double.PositiveInfinity);
    }
  }
}
