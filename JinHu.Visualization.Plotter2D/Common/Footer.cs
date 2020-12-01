using System.Windows;
using System.Windows.Controls;

namespace JinHu.Visualization.Plotter2D
{
  /// <summary>
  ///   Represents a text in Plotter's footer.
  /// </summary>
  public class Footer : ContentControl, IPlotterElement
  {
    /// <summary>
    ///   Initializes a new instance of the <see cref="Footer"/> class.
    /// </summary>
    public Footer()
    {
      HorizontalAlignment = HorizontalAlignment.Center;
      Margin = new Thickness(0, 0, 0, 3);
    }

    void IPlotterElement.OnPlotterAttached(PlotterBase _plotter)
    {
      Plotter = _plotter;
      _plotter.FooterPanel.Children.Add(this);
    }

    void IPlotterElement.OnPlotterDetaching(PlotterBase _plotter)
    {
      _plotter.FooterPanel.Children.Remove(this);
      Plotter = null;
    }

    public PlotterBase Plotter { get; private set; }
  }
}
