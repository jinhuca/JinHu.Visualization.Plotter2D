using System.Windows;

namespace JinHu.Visualization.Plotter2D
{
  public static class PointExtensions
  {
    public static Vector ToVector(this Point pt) => new Vector(pt.X, pt.Y);

    public static bool IsFinite(this Point pt) => pt.X.IsFinite() && pt.Y.IsFinite();
  }
}
