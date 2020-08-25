﻿using System.Windows;

namespace JinHu.Visualization.Plotter2D
{
  /// <summary>
  /// Target of rendering
  /// </summary>
  public enum RenderTo
  {
    /// <summary>
    /// Rendering directly to screen
    /// </summary>
    Screen,
    /// <summary>
    /// Rendering to bitmap, which will be drawn to screen later.
    /// </summary>
    Image
  }

  public sealed class RenderState
  {
    public DataRect RenderVisible { get; }
    public RenderTo RenderingType { get; }
    public Rect Output { get; }
    public DataRect Visible { get; }

    internal RenderState(DataRect renderVisible, DataRect visible, Rect output, RenderTo renderingType)
    {
      RenderVisible = renderVisible;
      Visible = visible;
      Output = output;
      RenderingType = renderingType;
    }
  }
}
