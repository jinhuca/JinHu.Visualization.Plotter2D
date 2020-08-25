using System.Diagnostics;
using System.Windows;

namespace JinHu.Visualization.Plotter2D
{
  public static class RectExtensions
  {
    public static Point GetCenter(this Rect rect) => new Point(rect.Left + rect.Width * 0.5, rect.Top + rect.Height * 0.5);

    public static Rect FromCenterSize(Point center, Size size) => FromCenterSize(center, size.Width, size.Height);

    public static Rect FromCenterSize(Point center, double width, double height) => new Rect(center.X - width / 2, center.Y - height / 2, width, height);

    public static Rect Zoom(this Rect rect, Point to, double ratio) => CoordinateUtilities.RectZoom(rect, to, ratio);

    public static Rect ZoomOutFromCenter(this Rect rect, double ratio) => CoordinateUtilities.RectZoom(rect, rect.GetCenter(), ratio);

    public static Rect ZoomInToCenter(this Rect rect, double ratio) => CoordinateUtilities.RectZoom(rect, rect.GetCenter(), 1 / ratio);

    public static Int32Rect ToInt32Rect(this Rect rect) => new Int32Rect((int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height);

    [DebuggerStepThrough]
    public static DataRect ToDataRect(this Rect rect) => new DataRect(rect);

    internal static bool IsNaN(this Rect rect) => !rect.IsEmpty && (rect.X.IsNaN() || rect.Y.IsNaN() || rect.Width.IsNaN() || rect.Height.IsNaN());
  }
}
