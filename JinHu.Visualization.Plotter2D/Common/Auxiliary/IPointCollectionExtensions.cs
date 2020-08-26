using System.Collections.Generic;
using System.Windows;

namespace JinHu.Visualization.Plotter2D
{
  public static class IPointCollectionExtensions
  {
    public static DataRect GetBounds(this IEnumerable<Point> points)
    {
      return BoundsHelper.GetViewportBounds(points);
    }

    public static IEnumerable<Point> Skip(this IList<Point> points, int skipCount)
    {
      for (int i = skipCount; i < points.Count; i++)
      {
        yield return points[i];
      }
    }
  }
}
