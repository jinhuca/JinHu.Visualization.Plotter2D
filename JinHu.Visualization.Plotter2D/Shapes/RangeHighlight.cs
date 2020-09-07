using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace JinHu.Visualization.Plotter2D.Charts
{
  /// <summary>
  /// Represents rectangle with corners bound to viewport coordinates.
  /// </summary>
  [TemplatePart(Name = "PART_LinesPath", Type = typeof(Path))]
  [TemplatePart(Name = "PART_RectPath", Type = typeof(Path))]
  public abstract class RangeHighlight : Control, IPlotterElement
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="RangeHighlight"/> class.
    /// </summary>
    protected RangeHighlight()
    {
      Resources = new ResourceDictionary { Source = new Uri(uriString: Constants.ShapeResourceUri, uriKind: UriKind.Relative) };

      Style = (Style)FindResource(resourceKey: typeof(RangeHighlight));
      ApplyTemplate();
    }

    bool partsLoaded = false;
    protected bool PartsLoaded
    {
      get { return partsLoaded; }
    }
    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();

      linesPath = (Path)Template.FindName(name: "PART_LinesPath", templatedParent: this);
      GeometryGroup linesGroup = new GeometryGroup();
      linesGroup.Children.Add(value: lineGeometry1);
      linesGroup.Children.Add(value: lineGeometry2);
      linesPath.Data = linesGroup;

      rectPath = (Path)Template.FindName(name: "PART_RectPath", templatedParent: this);
      rectPath.Data = rectGeometry;

      partsLoaded = true;
    }

    #region Presentation DPs

    public static readonly DependencyProperty FillProperty = DependencyProperty.Register(
      name: "InnerBrush",
      propertyType: typeof(Brush),
      ownerType: typeof(RangeHighlight),
      typeMetadata: new PropertyMetadata(defaultValue: Shape.FillProperty.DefaultMetadata.DefaultValue));

    public Brush Fill
    {
      get { return (Brush)GetValue(dp: FillProperty); }
      set { SetValue(dp: FillProperty, value: value); }
    }

    public static readonly DependencyProperty StrokeProperty = DependencyProperty.Register(
      name: "Stroke",
      propertyType: typeof(Brush),
      ownerType: typeof(RangeHighlight),
      typeMetadata: new PropertyMetadata(defaultValue: Shape.StrokeProperty.DefaultMetadata.DefaultValue));

    public Brush Stroke
    {
      get { return (Brush)GetValue(dp: StrokeProperty); }
      set { SetValue(dp: StrokeProperty, value: value); }
    }

    public static readonly DependencyProperty StrokeThicknessProperty = DependencyProperty.Register(
      name: "StrokeThickness",
      propertyType: typeof(double),
      ownerType: typeof(RangeHighlight),
      typeMetadata: new PropertyMetadata(defaultValue: Shape.StrokeThicknessProperty.DefaultMetadata.DefaultValue));

    public double StrokeThickness
    {
      get { return (double)GetValue(dp: StrokeThicknessProperty); }
      set { SetValue(dp: StrokeThicknessProperty, value: value); }
    }

    public static readonly DependencyProperty StrokeStartLineCapProperty = DependencyProperty.Register(
      name: "StrokeStartLineCap",
      propertyType: typeof(PenLineCap),
      ownerType: typeof(RangeHighlight),
      typeMetadata: new PropertyMetadata(defaultValue: Shape.StrokeStartLineCapProperty.DefaultMetadata.DefaultValue));

    public PenLineCap StrokeStartLineCap
    {
      get { return (PenLineCap)GetValue(dp: StrokeStartLineCapProperty); }
      set { SetValue(dp: StrokeStartLineCapProperty, value: value); }
    }

    public static readonly DependencyProperty StrokeEndLineCapProperty = DependencyProperty.Register(
      name: "StrokeEndLineCap",
      propertyType: typeof(PenLineCap),
      ownerType: typeof(RangeHighlight),
      typeMetadata: new PropertyMetadata(defaultValue: Shape.StrokeEndLineCapProperty.DefaultMetadata.DefaultValue));

    public PenLineCap StrokeEndLineCap
    {
      get { return (PenLineCap)GetValue(dp: StrokeEndLineCapProperty); }
      set { SetValue(dp: StrokeEndLineCapProperty, value: value); }
    }

    public static readonly DependencyProperty StrokeDashCapProperty = DependencyProperty.Register(
      name: "StrokeDashCap",
      propertyType: typeof(PenLineCap),
      ownerType: typeof(RangeHighlight),
      typeMetadata: new PropertyMetadata(defaultValue: Shape.StrokeDashCapProperty.DefaultMetadata.DefaultValue));

    public PenLineCap StrokeDashCap
    {
      get { return (PenLineCap)GetValue(dp: StrokeDashCapProperty); }
      set { SetValue(dp: StrokeDashCapProperty, value: value); }
    }

    public static readonly DependencyProperty StrokeLineJoinProperty = DependencyProperty.Register(
      name: "StrokeLineJoin",
      propertyType: typeof(PenLineJoin),
      ownerType: typeof(RangeHighlight),
      typeMetadata: new PropertyMetadata(defaultValue: Shape.StrokeLineJoinProperty.DefaultMetadata.DefaultValue));

    public PenLineJoin StrokeLineJoin
    {
      get { return (PenLineJoin)GetValue(dp: StrokeLineJoinProperty); }
      set { SetValue(dp: StrokeLineJoinProperty, value: value); }
    }

    public static readonly DependencyProperty StrokeMiterLimitProperty = DependencyProperty.Register(
      name: "StrokeMiterLimit",
      propertyType: typeof(double),
      ownerType: typeof(RangeHighlight),
      typeMetadata: new PropertyMetadata(defaultValue: Shape.StrokeMiterLimitProperty.DefaultMetadata.DefaultValue));

    public double StrokeMiterLimit
    {
      get { return (double)GetValue(dp: StrokeMiterLimitProperty); }
      set { SetValue(dp: StrokeMiterLimitProperty, value: value); }
    }

    public static readonly DependencyProperty StrokeDashOffsetProperty = DependencyProperty.Register(
      name: "StrokeDashOffset",
      propertyType: typeof(double),
      ownerType: typeof(RangeHighlight),
      typeMetadata: new PropertyMetadata(defaultValue: Shape.StrokeDashOffsetProperty.DefaultMetadata.DefaultValue));

    public double StrokeDashOffset
    {
      get { return (double)GetValue(dp: StrokeDashOffsetProperty); }
      set { SetValue(dp: StrokeDashOffsetProperty, value: value); }
    }

    public static readonly DependencyProperty StrokeDashArrayProperty = DependencyProperty.Register(
      name: "StrokeDashArray",
      propertyType: typeof(DoubleCollection),
      ownerType: typeof(RangeHighlight),
      typeMetadata: new PropertyMetadata(defaultValue: Shape.StrokeDashArrayProperty.DefaultMetadata.DefaultValue));

    [SuppressMessage(category: "Microsoft.Usage", checkId: "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public DoubleCollection StrokeDashArray
    {
      get { return (DoubleCollection)GetValue(dp: StrokeDashArrayProperty); }
      set { SetValue(dp: StrokeDashArrayProperty, value: value); }
    }

    #endregion

    #region Values dependency properties

    /// <summary>
    /// Gets or sets the first value determining position of rectangle in viewport coordinates.
    /// </summary>
    /// <value>The value1.</value>
    public double Value1
    {
      get { return (double)GetValue(dp: Value1Property); }
      set { SetValue(dp: Value1Property, value: value); }
    }

    public static readonly DependencyProperty Value1Property =
      DependencyProperty.Register(
        name: "Value1",
        propertyType: typeof(double),
        ownerType: typeof(RangeHighlight),
        typeMetadata: new FrameworkPropertyMetadata(defaultValue: 0.0, propertyChangedCallback: OnValueChanged));

    private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      RangeHighlight r = (RangeHighlight)d;
      r.OnValueChanged(e: e);
    }

    /// <summary>
    /// Gets or sets the second value determining position of rectangle in viewport coordinates.
    /// </summary>
    /// <value>The value2.</value>
    public double Value2
    {
      get { return (double)GetValue(dp: Value2Property); }
      set { SetValue(dp: Value2Property, value: value); }
    }

    public static readonly DependencyProperty Value2Property =
      DependencyProperty.Register(
        name: "Value2",
        propertyType: typeof(double),
        ownerType: typeof(RangeHighlight),
        typeMetadata: new FrameworkPropertyMetadata(defaultValue: 0.0, propertyChangedCallback: OnValueChanged));

    private void OnValueChanged(DependencyPropertyChangedEventArgs e)
    {
      UpdateUIRepresentation();
    }

    #endregion

    #region Geometry

    private Path rectPath;
    private Path linesPath;

    private readonly RectangleGeometry rectGeometry = new RectangleGeometry();
    protected RectangleGeometry RectGeometry
    {
      get { return rectGeometry; }
    }

    private readonly LineGeometry lineGeometry1 = new LineGeometry();
    protected LineGeometry LineGeometry1
    {
      get { return lineGeometry1; }
    }

    private readonly LineGeometry lineGeometry2 = new LineGeometry();
    protected LineGeometry LineGeometry2
    {
      get { return lineGeometry2; }
    }

    #endregion

    #region IPlotterElement Members

    private PlotterBase plotter;
    void IPlotterElement.OnPlotterAttached(PlotterBase plotter)
    {
      plotter.CentralGrid.Children.Add(element: this);

      PlotterBase plotter2d = (PlotterBase)plotter;
      this.plotter = plotter2d;
      plotter2d.Viewport.PropertyChanged += Viewport_PropertyChanged;

      UpdateUIRepresentation();
    }

    private void UpdateUIRepresentation()
    {
      if (Plotter == null)
      {
        return;
      }

      if (partsLoaded)
      {
        UpdateUIRepresentationCore();
      }
    }
    protected virtual void UpdateUIRepresentationCore() { }

    void Viewport_PropertyChanged(object sender, ExtendedPropertyChangedEventArgs e)
    {
      UpdateUIRepresentation();
    }

    void IPlotterElement.OnPlotterDetaching(PlotterBase plotter)
    {
      PlotterBase plotter2d = (PlotterBase)plotter;
      plotter2d.Viewport.PropertyChanged -= Viewport_PropertyChanged;
      plotter.CentralGrid.Children.Remove(element: this);

      this.plotter = null;
    }

    public PlotterBase Plotter
    {
      get { return plotter; }
    }

    PlotterBase IPlotterElement.Plotter
    {
      get { return plotter; }
    }

    #endregion
  }
}
