using System;
using System.Windows;

namespace JinHu.Visualization.Plotter2D
{
  public static class MathHelper
  {
    public static long Clamp(long value, long min, long max) => Math.Max(min, Math.Min(value, max));

    public static double Clamp(double value, double min, double max) => Math.Max(min, Math.Min(value, max));

    /// <summary>
    ///   Clamps specified value to [0,1].
    /// </summary>
    /// <param name="d">
    ///   Value to clamp.
    /// </param>
    /// <returns>
    ///   Value in range [0,1].
    /// </returns>
    public static double Clamp(double value) => Math.Max(0, Math.Min(value, 1));

    public static int Clamp(int value, int min, int max) => Math.Max(min, Math.Min(value, max));

    public static Rect CreateRectByPoints(double xMin, double yMin, double xMax, double yMax) => new Rect(new Point(xMin, yMin), new Point(xMax, yMax));

    public static double Interpolate(double start, double end, double ratio) => start * (1 - ratio) + end * ratio;

    public static double RadiansToDegrees(this double radians) => radians * 180 / Math.PI;

    public static double DegreesToRadians(this double degrees) => degrees / 180 * Math.PI;

    /// <summary>
    ///   Converts vector into angle.
    /// </summary>
    /// <param name="vector">
    ///   The vector.
    /// </param>
    /// <returns>
    ///   Angle in degrees.
    /// </returns>
    public static double ToAngle(this Vector vector) => Math.Atan2(-vector.Y, vector.X).RadiansToDegrees();

    public static Point ToPoint(this Vector v) => new Point(v.X, v.Y);

    public static bool IsNaN(this double d) => double.IsNaN(d);

    public static bool IsNotNaN(this double d) => !double.IsNaN(d);

    public static bool IsFinite(this double d) => !double.IsNaN(d) && !double.IsInfinity(d);

    public static bool IsInfinite(this double d) => double.IsInfinity(d);

    public static bool AreClose(double d1, double d2, double diffRatio) => Math.Abs(d1 / d2 - 1) < diffRatio;
  }
}