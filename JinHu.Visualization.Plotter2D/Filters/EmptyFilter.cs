using System.Collections.Generic;
using System.Windows;

namespace JinHu.Visualization.Plotter2D.Charts
{
  public sealed class EmptyFilter : PointsFilterBase
  {
    public override List<Point> Filter(List<Point> points)
    {
      return points;
    }
  }
}
