using System;
using System.Collections.Generic;
using System.Windows;

namespace JinHu.Visualization.Plotter2D
{
  public static class BoundsHelper
  {
    /// <summary>
    ///   Computes bounding rectangle for sequence of points.
    /// </summary>
    /// <param name="points">
    ///   Points sequence.
    /// </param>
    /// <returns>
    ///   Minimal axis-aligned bounding rectangle.
    /// </returns>
    public static DataRect GetViewportBounds(IEnumerable<Point> viewportPoints)
    {
      var bounds = DataRect.Empty;
      var xMin = double.PositiveInfinity;
      var xMax = double.NegativeInfinity;
      var yMin = double.PositiveInfinity;
      var yMax = double.NegativeInfinity;

      foreach (var p in viewportPoints)
      {
        xMin = Math.Min(xMin, p.X);
        xMax = Math.Max(xMax, p.X);

        yMin = Math.Min(yMin, p.Y);
        yMax = Math.Max(yMax, p.Y);
      }

      // were some points in collection
      if (!double.IsInfinity(xMin))
      {
        bounds = DataRect.Create(xMin, yMin, xMax, yMax);
      }

      return bounds;
    }

    public static DataRect GetViewportBounds(IEnumerable<Point> dataPoints, DataTransform transform)
    {
      return GetViewportBounds(dataPoints.DataToViewport(transform));
    }
  }
}
