using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace JinHu.Visualization.Plotter2D.Charts
{
  /// <summary>
  /// Represents a base class for simple shapes with viewport-bound coordinates.
  /// </summary>
  public abstract class ViewportShape : Shape, IPlotterElement
  {
    static ViewportShape()
    {
      Type type = typeof(ViewportShape);
      StrokeProperty.AddOwner(type, new FrameworkPropertyMetadata(Brushes.Blue));
      StrokeThicknessProperty.AddOwner(type, new FrameworkPropertyMetadata(2.0));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ViewportShape"/> class.
    /// </summary>
    protected ViewportShape() { }

    protected void UpdateUIRepresentation()
    {
      if (Plotter == null)
      {
        return;
      }

      UpdateUIRepresentationCore();
    }
    protected virtual void UpdateUIRepresentationCore() { }

    #region IPlotterElement Members

    private PlotterBase plotter;
    void IPlotterElement.OnPlotterAttached(PlotterBase plotter)
    {
      plotter.CentralGrid.Children.Add(this);

      PlotterBase plotter2d = (PlotterBase)plotter;
      this.plotter = plotter2d;
      plotter2d.Viewport.PropertyChanged += Viewport_PropertyChanged;

      UpdateUIRepresentation();
    }

    private void Viewport_PropertyChanged(object sender, ExtendedPropertyChangedEventArgs e)
    {
      OnViewportPropertyChanged(e);
    }

    protected virtual void OnViewportPropertyChanged(ExtendedPropertyChangedEventArgs e)
    {
      UpdateUIRepresentation();
    }

    void IPlotterElement.OnPlotterDetaching(PlotterBase plotter)
    {
      PlotterBase plotter2d = (PlotterBase)plotter;
      plotter2d.Viewport.PropertyChanged -= Viewport_PropertyChanged;
      plotter.CentralGrid.Children.Remove(this);

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
