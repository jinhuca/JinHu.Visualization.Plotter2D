using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace JinHu.Visualization.Plotter2D
{
  /// <summary>
  ///   Adds Circle element at every point of graph.
  /// </summary>
  public class RectElementPointMarker : ShapeElementPointMarker
  {
    public override UIElement CreateMarker()
    {
      Rectangle result = new Rectangle();
      result.Width = Size;
      result.Height = Size;
      result.Stroke = Brush;
      result.Fill = Fill;
      if (!string.IsNullOrEmpty(ToolTipText))
      {
        result.ToolTip = new ToolTip
        {
          Content = ToolTipText
        };
      }
      return result;
    }

    public override void SetMarkerProperties(UIElement marker)
    {
      Rectangle rect = (Rectangle)marker;

      rect.Width = Size;
      rect.Height = Size;
      rect.Stroke = Brush;
      rect.Fill = Fill;

      if (!string.IsNullOrEmpty(ToolTipText))
      {
        rect.ToolTip = new ToolTip
        {
          Content = ToolTipText
        };
      }
    }

    public override void SetPosition(UIElement marker, Point screenPoint)
    {
      Canvas.SetLeft(marker, screenPoint.X - Size / 2);
      Canvas.SetTop(marker, screenPoint.Y - Size / 2);
    }
  }
}
