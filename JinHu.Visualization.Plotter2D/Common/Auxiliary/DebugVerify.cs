using System;
using System.Diagnostics;
using System.Windows;

namespace JinHu.Visualization.Plotter2D.Common
{
  internal static class DebugVerify
  {
    [Conditional("DEBUG")]
    [DebuggerStepThrough]
    public static void Is(bool condition)
    {
      if (!condition)
      {
        throw new ArgumentException(Strings.Exceptions.AssertionFailed);
      }
    }

    [Conditional("DEBUG")]
    [DebuggerStepThrough]
    public static void IsNotNaN(double d)
    {
      Is(!double.IsNaN(d));
    }

    [Conditional("DEBUG")]
    [DebuggerStepThrough]
    public static void IsNotNaN(Vector vec)
    {
      IsNotNaN(vec.X);
      IsNotNaN(vec.Y);
    }

    [Conditional("DEBUG")]
    [DebuggerStepThrough]
    public static void IsNotNaN(Point point)
    {
      IsNotNaN(point.X);
      IsNotNaN(point.Y);
    }

    [Conditional("DEBUG")]
    [DebuggerStepThrough]
    public static void IsFinite(double d)
    {
      Is(!double.IsInfinity(d) && !(double.IsNaN(d)));
    }

    [Conditional("DEBUG")]
    [DebuggerStepThrough]
    public static void IsNotNull(object obj)
    {
      Is(obj != null);
    }
  }
}
