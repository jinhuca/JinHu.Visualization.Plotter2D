using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace JinHu.Visualization.Plotter2D.Charts
{
  /// <summary>
  ///   Represents a menu that appears in Debug version of JinHu.Visualization.Plotter2D.
  /// </summary>
  public class DebugMenu : IPlotterElement
  {
    /// <summary>
    ///   Initializes a new instance of the <see cref="DebugMenu"/> class.
    /// </summary>
    public DebugMenu()
    {
      Panel.SetZIndex(Menu, 1);
    }

    public Menu Menu { get; } = new Menu
    {
      HorizontalAlignment = HorizontalAlignment.Left,
      VerticalAlignment = VerticalAlignment.Top,
      Margin = new Thickness(3)
    };

    public MenuItem TryFindMenuItem(string itemName)
    {
      return Menu.Items.OfType<MenuItem>().FirstOrDefault(item => item.Header.ToString() == itemName);
    }

    #region IPlotterElement Members

    public void OnPlotterAttached(PlotterBase plotter)
    {
      Plotter = plotter;
      plotter.CentralGrid.Children.Add(Menu);
    }

    public void OnPlotterDetaching(PlotterBase plotter)
    {
      plotter.CentralGrid.Children.Remove(Menu);
      Plotter = null;
    }

    public PlotterBase Plotter { get; private set; }

    #endregion
  }
}
