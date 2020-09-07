using System.Windows;
using System.Windows.Media;

namespace JinHu.Visualization.Plotter2D
{
  /// <summary>
  /// Class that Renders circle around each point of graph.
  /// </summary>
  public class CirclePointMarker : ShapePointMarker
  {
    public override void Render(DrawingContext dc, Point screenPoint)
    {
      dc.DrawEllipse(FillBrush, OutlinePen, screenPoint, Diameter / 2, Diameter / 2);
    }
  }
}

