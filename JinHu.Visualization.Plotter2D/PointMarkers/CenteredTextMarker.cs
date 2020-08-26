using System.Threading;
using System.Windows;
using System.Windows.Media;

namespace JinHu.Visualization.Plotter2D
{
  /// <summary>
  /// Renders specified text near the point
  /// </summary>
  public class CenteredTextMarker : PointMarker
  {
    public string Text
    {
      get { return (string)GetValue(TextProperty); }
      set { SetValue(TextProperty, value); }
    }

    private const string TypefaceName = "Arial";
    public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
      "Text",
      typeof(string),
      typeof(CenteredTextMarker),
      new FrameworkPropertyMetadata(""));

    public override void Render(DrawingContext dc, Point screenPoint)
    {
#pragma warning disable CS0618 // 'FormattedText.FormattedText(string, CultureInfo, FlowDirection, Typeface, double, Brush)' is obsolete: 'Use the PixelsPerDip override'
      FormattedText textToDraw = new FormattedText(
        Text,
        Thread.CurrentThread.CurrentCulture,
        FlowDirection.LeftToRight,
        new Typeface(TypefaceName), 12, Brushes.Black);
#pragma warning restore CS0618 // 'FormattedText.FormattedText(string, CultureInfo, FlowDirection, Typeface, double, Brush)' is obsolete: 'Use the PixelsPerDip override'

      double width = textToDraw.Width;
      double height = textToDraw.Height;

      const double verticalShift = -20; // px

      Rect bounds = RectExtensions.FromCenterSize(
        new Point(screenPoint.X, screenPoint.Y + verticalShift - height / 2),
        new Size(width, height));

      Point loc = bounds.Location;
      bounds = CoordinateUtilities.RectZoom(bounds, 1.05, 1.15);

      dc.DrawLine(new Pen(Brushes.Black, 1), Point.Add(screenPoint, new Vector(0, verticalShift)), screenPoint);
      dc.DrawRectangle(Brushes.White, new Pen(Brushes.Black, 1), bounds);
      dc.DrawText(textToDraw, loc);
    }
  }
}