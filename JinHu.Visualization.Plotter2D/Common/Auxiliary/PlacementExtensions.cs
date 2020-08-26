using JinHu.Visualization.Plotter2D.Charts;

namespace JinHu.Visualization.Plotter2D.Common
{
  internal static class PlacementExtensions
  {
    public static bool IsBottomOrTop(this AxisPlacement placement)
    {
      return placement == AxisPlacement.Bottom || placement == AxisPlacement.Top;
    }
  }
}
