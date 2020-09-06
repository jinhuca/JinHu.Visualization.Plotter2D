using System.Windows;
using System.Windows.Media;

namespace JinHu.Visualization.Plotter2D
{
  /// <summary>
  /// Abstract class that extends PointMarker and contains marker property as Pen, Brush and Size.
  /// </summary>
  public abstract class ShapePointMarker : PointMarker
  {
    /// <summary>
    /// Size of marker in points.
    /// </summary>
    public double Size
    {
      get => (double)GetValue(dp: SizeProperty);
      set => SetValue(dp: SizeProperty, value: value);
    }

    public static readonly DependencyProperty SizeProperty = DependencyProperty.Register(
      name: nameof(Size),
      propertyType: typeof(double),
      ownerType: typeof(ShapePointMarker),
      typeMetadata: new FrameworkPropertyMetadata(defaultValue: 5.0));

    /// <summary>
    /// Pen to outline marker.
    /// </summary>
    public Pen Pen
    {
      get => (Pen)GetValue(dp: PenProperty);
      set => SetValue(dp: PenProperty, value: value);
    }

    public static readonly DependencyProperty PenProperty = DependencyProperty.Register(
      name: nameof(Pen),
      propertyType: typeof(Pen),
      ownerType: typeof(ShapePointMarker),
      typeMetadata: new FrameworkPropertyMetadata(propertyChangedCallback: null));

    public Brush Fill
    {
      get => (Brush)GetValue(dp: FillProperty);
      set => SetValue(dp: FillProperty, value: value);
    }

    public static readonly DependencyProperty FillProperty = DependencyProperty.Register(
      name: nameof(Fill),
      propertyType: typeof(Brush),
      ownerType: typeof(ShapePointMarker),
      typeMetadata: new FrameworkPropertyMetadata(defaultValue: Brushes.Red));
  }
}
