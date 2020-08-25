using System;
using System.Windows;

namespace JinHu.Visualization.Plotter2D.Common
{
  public class PlotterChangedEventArgs : RoutedEventArgs
  {
    public PlotterChangedEventArgs(PlotterBase prevPlotter, PlotterBase currPlotter, RoutedEvent routedEvent) : base(routedEvent)
    {
      if (prevPlotter == null && currPlotter == null)
      {
        throw new ArgumentException("Both Plotters cannot be null.");
      }

      PreviousPlotter = prevPlotter;
      CurrentPlotter = currPlotter;
    }
    public PlotterBase PreviousPlotter { get; }
    public PlotterBase CurrentPlotter { get; }
  }
}
