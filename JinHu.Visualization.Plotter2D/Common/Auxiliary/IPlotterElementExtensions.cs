using System;

namespace JinHu.Visualization.Plotter2D
{
  public static class IPlotterElementExtensions
  {
    public static void RemoveFromPlotter(this IPlotterElement element)
    {
      if (element == null)
      {
        throw new ArgumentNullException(nameof(element));
      }

      if (element.Plotter != null)
      {
        element.Plotter.Children.Remove(element);
      }
    }

    public static void AddToPlotter(this IPlotterElement element, PlotterBase plotter)
    {
      if (element == null)
      {
        throw new ArgumentNullException(nameof(element));
      }
      if (plotter == null)
      {
        throw new ArgumentNullException(nameof(plotter));
      }
      plotter.Children.Add(element);
    }
  }
}
