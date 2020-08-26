using System.Windows;

namespace JinHu.Visualization.Plotter2D
{
  public static class CoordinateUtilities
  {
    public static Rect RectZoom(Rect rect, double ratio)
      => RectZoom(rect, rect.GetCenter(), ratio);

    public static Rect RectZoom(Rect rect, double horizontalRatio, double verticalRatio)
      => RectZoom(rect, rect.GetCenter(), horizontalRatio, verticalRatio);

    public static Rect RectZoom(Rect rect, Point zoomCenter, double ratio)
      => RectZoom(rect, zoomCenter, ratio, ratio);

    public static Rect RectZoom(Rect rect, Point zoomCenter, double horizontalRatio, double verticalRatio)
    {
      Rect res = new Rect();
      res.X = zoomCenter.X - (zoomCenter.X - rect.X) * horizontalRatio;
      res.Y = zoomCenter.Y - (zoomCenter.Y - rect.Y) * verticalRatio;
      res.Width = rect.Width * horizontalRatio;
      res.Height = rect.Height * verticalRatio;
      return res;
    }

    public static DataRect RectZoom(DataRect rect, double ratio)
      => RectZoom(rect, rect.GetCenter(), ratio);

    public static DataRect RectZoom(DataRect rect, double horizontalRatio, double verticalRatio)
      => RectZoom(rect, rect.GetCenter(), horizontalRatio, verticalRatio);

    public static DataRect RectZoom(DataRect rect, Point zoomCenter, double ratio)
      => RectZoom(rect, zoomCenter, ratio, ratio);

    public static DataRect RectZoom(DataRect rect, Point zoomCenter, double horizontalRatio, double verticalRatio)
    {
      DataRect res = new DataRect();
      res.XMin = zoomCenter.X - (zoomCenter.X - rect.XMin) * horizontalRatio;
      res.YMin = zoomCenter.Y - (zoomCenter.Y - rect.YMin) * verticalRatio;
      res.Width = rect.Width * horizontalRatio;
      res.Height = rect.Height * verticalRatio;
      return res;
    }

    public static DataRect RectZoomX(DataRect rect, Point zoomCenter, double ratio)
    {
      DataRect res = rect;
      res.XMin = zoomCenter.X - (zoomCenter.X - rect.XMin) * ratio;
      res.Width = rect.Width * ratio;
      return res;
    }

    public static DataRect RectZoomY(DataRect rect, Point zoomCenter, double ratio)
    {
      DataRect res = rect;
      res.YMin = zoomCenter.Y - (zoomCenter.Y - rect.YMin) * ratio;
      res.Height = rect.Height * ratio;
      return res;
    }
  }
}
