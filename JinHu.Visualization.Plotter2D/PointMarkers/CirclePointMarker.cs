﻿using System.Windows;
using System.Windows.Media;

namespace JinHu.Visualization.Plotter2D
{
  /// <summary>
  ///   Renders circle around each point of graph.
  /// </summary>
  public class CirclePointMarker : ShapePointMarker
  {
    public override void Render(DrawingContext dc, Point screenPoint)
    {
      dc.DrawEllipse(Fill, Pen, screenPoint, Size / 2, Size / 2);
    }
  }
}
