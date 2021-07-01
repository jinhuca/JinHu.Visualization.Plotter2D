﻿using System;
using System.Diagnostics;
using System.Windows;

namespace JinHu.Visualization.Plotter2D
{
  /// <summary>
  /// A central class in 2d coordinate transformation in Plotter2D.
  /// Provides methods to transform point from one coordinate system to another.
  /// Should be immutable.
  /// </summary>
  public sealed class CoordinateTransform
  {
    private CoordinateTransform(DataRect visibleRect, Rect screenRect)
    {
      VisibleRect = visibleRect;
      ScreenRect1 = screenRect;

      rxToScreen = screenRect.Width / visibleRect.Width;
      ryToScreen = screenRect.Height / visibleRect.Height;
      cxToScreen = visibleRect.XMin * rxToScreen - screenRect.Left;
      cyToScreen = screenRect.Height + screenRect.Top + visibleRect.YMin * ryToScreen;

      rxToData = visibleRect.Width / screenRect.Width;
      ryToData = visibleRect.Height / screenRect.Height;
      cxToData = screenRect.Left * rxToData - visibleRect.XMin;
      cyToData = visibleRect.Height + visibleRect.YMin + screenRect.Top * ryToData;
    }

    #region Coeffs

    readonly double rxToScreen;
    readonly double ryToScreen;
    readonly double cxToScreen;
    readonly double cyToScreen;

    readonly double rxToData;
    readonly double ryToData;
    readonly double cxToData;
    readonly double cyToData;
    #endregion

    #region Creation methods

    internal static CoordinateTransform FromRects(DataRect visibleRect, Rect screenRect)
    {
      CoordinateTransform result = new CoordinateTransform(visibleRect, screenRect);
      return result;
    }

    internal CoordinateTransform WithRects(DataRect visibleRect, Rect screenRect)
    {
      CoordinateTransform copy = new CoordinateTransform(visibleRect, screenRect);
      copy.dataTransform = dataTransform;
      return copy;
    }

    /// <summary>
    /// Creates a new instance of CoordinateTransform with the given data transform.
    /// </summary>
    /// <param name="dataTransform">The data transform.</param>
    /// <returns></returns>
    public CoordinateTransform WithDataTransform(DataTransform dataTransform)
    {
      return new CoordinateTransform(VisibleRect, ScreenRect1)
      {
        dataTransform = dataTransform ?? throw new ArgumentNullException("dataTransform")
      };
    }

    internal CoordinateTransform WithScreenOffset(double x, double y)
    {
      Rect screenCopy = ScreenRect1;
      screenCopy.Offset(x, y);
      CoordinateTransform copy = new CoordinateTransform(VisibleRect, screenCopy);
      return copy;
    }

    internal static CoordinateTransform CreateDefault() => new CoordinateTransform(new Rect(0, 0, 1, 1), new Rect(0, 0, 1, 1));

    #endregion

    #region Transform methods

    /// <summary>
    /// Transforms point from data coordinates to screen.
    /// </summary>
    /// <param name="dataPoint">The point in data coordinates.</param>
    /// <returns></returns>
    public Point DataToScreen(Point dataPoint)
    {
      Point viewportPoint = dataTransform.DataToViewport(dataPoint);

      Point screenPoint = new Point(viewportPoint.X * rxToScreen - cxToScreen,
          cyToScreen - viewportPoint.Y * ryToScreen);

      return screenPoint;
    }

    /// <summary>
    /// Transforms point from screen coordinates to data coordinates.
    /// </summary>
    /// <param name="screenPoint">The point in screen coordinates.</param>
    /// <returns></returns>
    public Point ScreenToData(Point screenPoint)
    {
      Point viewportPoint = new Point(screenPoint.X * rxToData - cxToData,
          cyToData - screenPoint.Y * ryToData);

      Point dataPoint = dataTransform.ViewportToData(viewportPoint);

      return dataPoint;
    }

    /// <summary>
    /// Transforms point from viewport coordinates to screen coordinates.
    /// </summary>
    /// <param name="viewportPoint">The point in viewport coordinates.</param>
    /// <returns></returns>
    public Point ViewportToScreen(Point viewportPoint)
    {
      Point screenPoint = new Point(viewportPoint.X * rxToScreen - cxToScreen,
          cyToScreen - viewportPoint.Y * ryToScreen);

      return screenPoint;
    }

    /// <summary>
    /// Transforms point from screen coordinates to viewport coordinates.
    /// </summary>
    /// <param name="screenPoint">The point in screen coordinates.</param>
    /// <returns></returns>
    public Point ScreenToViewport(Point screenPoint)
    {
      Point viewportPoint = new Point(screenPoint.X * rxToData - cxToData,
          cyToData - screenPoint.Y * ryToData);

      return viewportPoint;
    }

    #endregion

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private DataRect visibleRect;
    /// <summary>
    /// Gets the viewport rectangle.
    /// </summary>
    /// <value>The viewport rect.</value>
    public DataRect ViewportRect => VisibleRect;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private Rect screenRect;

    /// <summary>
    /// Gets the screen rectangle.
    /// </summary>
    /// <value>The screen rect.</value>
    public Rect ScreenRect => ScreenRect1;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private DataTransform dataTransform = DataTransforms.Identity;
    /// <summary>
    /// Gets the data transform.
    /// </summary>
    /// <value>The data transform.</value>
    public DataTransform DataTransform => dataTransform;

    public DataRect VisibleRect { get => visibleRect; set => visibleRect = value; }
    public Rect ScreenRect1 { get => screenRect; set => screenRect = value; }
  }
}
