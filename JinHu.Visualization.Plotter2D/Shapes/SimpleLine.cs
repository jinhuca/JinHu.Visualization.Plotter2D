﻿using System.Windows;
using System.Windows.Media;

namespace JinHu.Visualization.Plotter2D.Charts
{
  /// <summary>
  /// Represents simple line bound to viewport coordinates.
  /// </summary>
  public abstract class SimpleLine : ViewportShape
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="SimpleLine"/> class.
    /// </summary>
    protected SimpleLine() { }

    /// <summary>
    /// Gets or sets the value of line - e.g., its horizontal or vertical coordinate.
    /// </summary>
    /// <value>The value.</value>
    public double Value
    {
      get { return (double)GetValue(ValueProperty); }
      set { SetValue(ValueProperty, value); }
    }

    /// <summary>
    /// Identifies Value dependency property.
    /// </summary>
    public static readonly DependencyProperty ValueProperty =
      DependencyProperty.Register(
        "Value",
        typeof(double),
        typeof(SimpleLine),
        new PropertyMetadata(
          0.0, OnValueChanged));

    private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      SimpleLine line = (SimpleLine)d;
      line.OnValueChanged();
    }

    protected virtual void OnValueChanged()
    {
      UpdateUIRepresentation();
    }

    private readonly LineGeometry lineGeometry = new LineGeometry();
    protected LineGeometry LineGeometry
    {
      get { return lineGeometry; }
    }
    protected override Geometry DefiningGeometry
    {
      get { return lineGeometry; }
    }
  }
}
