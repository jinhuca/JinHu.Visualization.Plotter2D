using JinHu.Visualization.Plotter2D.Charts;
using System;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace JinHu.Visualization.Plotter2D
{
  public abstract class PlotterScrollBar : IPlotterElement
  {
    protected PlotterScrollBar()
    {
      scrollBar.Scroll += OnScroll;
    }

    private void OnScroll(object sender, ScrollEventArgs e)
    {
      DataRect visible = plotter.Viewport.Visible;
      visible = CreateVisibleRect(visible, scrollBar.Value);
      plotter.Viewport.Visible = visible;
    }

    private readonly ScrollBar scrollBar = new ScrollBar();
    protected ScrollBar ScrollBar
    {
      get { return scrollBar; }
    }

    private PlotterBase plotter;
    protected PlotterBase Plotter
    {
      get { return plotter; }
    }

    private void OnViewportPropertyChanged(object sender, ExtendedPropertyChangedEventArgs e)
    {
      if (e.PropertyName == "Visible" || e.PropertyName == "Output")
      {
        UpdateScrollBar((Viewport2D)sender);
      }
    }

    #region IPlotterElement Members

    void IPlotterElement.OnPlotterAttached(PlotterBase plotter)
    {
      GetHostPanel(plotter).Children.Add(scrollBar);

      PlotterBase plotter2d = (PlotterBase)plotter;
      this.plotter = plotter2d;
      var viewport = plotter2d.Viewport;
      viewport.PropertyChanged += OnViewportPropertyChanged;
      viewport.DomainChanged += OnViewportDomainChanged;

      UpdateScrollBar(viewport);
    }

    protected abstract void UpdateScrollBar(Viewport2D viewport);

    protected virtual void SetValue(Range<double> visibleRange, Range<double> domainRange)
    {
      scrollBar.Value = visibleRange.Min;
    }

    private void OnViewportDomainChanged(object sender, EventArgs e)
    {
      UpdateScrollBar((Viewport2D)sender);
    }

    protected abstract DataRect CreateVisibleRect(DataRect rect, double value);
    protected abstract Panel GetHostPanel(PlotterBase plotter);

    void IPlotterElement.OnPlotterDetaching(PlotterBase plotter)
    {
      this.plotter.Viewport.PropertyChanged -= OnViewportPropertyChanged;
      this.plotter.Viewport.DomainChanged -= OnViewportDomainChanged;

      GetHostPanel(plotter).Children.Remove(scrollBar);

      UpdateScrollBar(null);

      this.plotter = null;
    }

    PlotterBase IPlotterElement.Plotter
    {
      get { return plotter; }
    }

    #endregion
  }
}
