using JinHu.Visualization.Plotter2D.Common;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace JinHu.Visualization.Plotter2D.Charts
{
  [ContentProperty("Content")]
  public class ViewportUIContainer : DependencyObject, IPlotterElement
  {
    public ViewportUIContainer() { }

    public ViewportUIContainer(FrameworkElement content)
    {
      Content = content;
    }

    private FrameworkElement content;
    [NotNull]
    public FrameworkElement Content
    {
      get { return content; }
      set { content = value; }
    }

    #region IPlotterElement Members

    void IPlotterElement.OnPlotterAttached(PlotterBase _plotter)
    {
      plotter = _plotter;
      if (Content == null)
      {
        return;
      }

      var plotterPanel = GetPlotterPanel(Content);
      //Plotter.SetPlotter(Content, _plotter);

      if (plotterPanel == PlotterPanel.MainCanvas)
      {
        // if all four Canvas.{Left|Right|Top|Bottom} properties are not set,
        // and as we are adding by default content to MainCanvas, 
        // and since I like more when buttons are by default in right down corner - 
        // set bottom and right to 10.
        var left = Canvas.GetLeft(content);
        var top = Canvas.GetTop(content);
        var bottom = Canvas.GetBottom(content);
        var right = Canvas.GetRight(content);

        if (left.IsNaN() && right.IsNaN() && bottom.IsNaN() && top.IsNaN())
        {
          Canvas.SetBottom(content, 10.0);
          Canvas.SetRight(content, 10.0);
        }
        _plotter.MainCanvas.Children.Add(Content);
      }
    }

    void IPlotterElement.OnPlotterDetaching(PlotterBase _plotter)
    {
      if (Content != null)
      {
        var plotterPanel = GetPlotterPanel(Content);
        //Plotter.SetPlotter(Content, null);
        if (plotterPanel == PlotterPanel.MainCanvas)
        {
          _plotter.MainCanvas.Children.Remove(Content);
        }
      }

      plotter = null;
    }

    private PlotterBase plotter;
    PlotterBase IPlotterElement.Plotter
    {
      get { return plotter; }
    }

    #endregion

    [AttachedPropertyBrowsableForChildren]
    public static PlotterPanel GetPlotterPanel(DependencyObject obj)
    {
      return (PlotterPanel)obj.GetValue(PlotterPanelProperty);
    }

    public static void SetPlotterPanel(DependencyObject obj, PlotterPanel value)
    {
      obj.SetValue(PlotterPanelProperty, value);
    }

    public static readonly DependencyProperty PlotterPanelProperty = DependencyProperty.RegisterAttached(
      "PlotterPanel",
      typeof(PlotterPanel),
      typeof(ViewportUIContainer),
      new FrameworkPropertyMetadata(PlotterPanel.MainCanvas));
  }
}