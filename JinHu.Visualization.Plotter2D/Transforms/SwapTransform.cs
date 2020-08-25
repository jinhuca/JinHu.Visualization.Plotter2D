﻿using System.Windows;

namespace JinHu.Visualization.Plotter2D
{
  /// <summary>
  /// Represents a DataTransform that simply swaps points' coefficitiens from x to y and vice verca.
  /// </summary>
  public sealed class SwapTransform : DataTransform
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="SwapTransform"/> class.
    /// </summary>
    public SwapTransform() { }

    /// <summary>
    /// Transforms the point in data coordinates to viewport coordinates.
    /// </summary>
    /// <param name="pt">The point in data coordinates.</param>
    /// <returns>
    /// Transformed point in viewport coordinates.
    /// </returns>
    public override Point DataToViewport(Point pt) => new Point(pt.Y, pt.X);

    /// <summary>
    /// Transforms the point in viewport coordinates to data coordinates.
    /// </summary>
    /// <param name="pt">The point in viewport coordinates.</param>
    /// <returns>Transformed point in data coordinates.</returns>
    public override Point ViewportToData(Point pt) => new Point(pt.Y, pt.X);
  }
}
