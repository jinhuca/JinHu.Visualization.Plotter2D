using JinHu.Visualization.Plotter2D.Charts;
using System.Windows;

namespace JinHu.Visualization.Plotter2D
{
  public static class RangeExtensions
  {
    public static double GetLength(this Range<Point> range)
    {
      Point p1 = range.Min;
      Point p2 = range.Max;
      return (p1 - p2).Length;
    }

    public static double GetLength(this Range<double> range) => range.Max - range.Min;
  }
}
