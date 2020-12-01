using JinHu.Visualization.Plotter2D.Graphs;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace JinHu.Visualization.Plotter2D
{
  public sealed class PenDescription : StandardDescription
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="PenDescription"/> class.
    /// </summary>
    public PenDescription() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="PenDescription"/> class.
    /// </summary>
    /// <param name="description">Custom description.</param>
    public PenDescription(string description) : base(description) { }

    protected override LegendItem CreateLegendItemCore()
    {
      return new LineLegendItem(this);
    }

    protected override void AttachCore(UIElement graph)
    {
      base.AttachCore(graph);
      if (graph is LineGraph g)
      {
        SetBinding(StrokeProperty, new Binding(nameof(g.LinePen.Brush)) { Source = g });
        SetBinding(StrokeThicknessProperty, new Binding(nameof(g.LinePen.Thickness)) { Source = g });
      }
    }

    public Brush Stroke
    {
      get => (Brush)GetValue(StrokeProperty);
      set => SetValue(StrokeProperty, value);
    }

    public static readonly DependencyProperty StrokeProperty = DependencyProperty.Register(
      name: nameof(Stroke),
      typeof(Brush),
      typeof(PenDescription),
      new FrameworkPropertyMetadata(null));

    public double StrokeThickness
    {
      get => (double)GetValue(StrokeThicknessProperty);
      set => SetValue(StrokeThicknessProperty, value);
    }

    public static readonly DependencyProperty StrokeThicknessProperty = DependencyProperty.Register(
      name: nameof(StrokeThickness),
      typeof(double),
      typeof(PenDescription),
      new FrameworkPropertyMetadata(0.0));

  }
}
