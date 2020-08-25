namespace JinHu.Visualization.Plotter2D.Common
{
  public static class PlotterExtensions
  {
    public static void AddChild(this PlotterBase plotter, IPlotterElement child)
    {
      plotter.Children.Add(child);
    }
  }
}
