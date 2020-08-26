using System.Windows;

namespace JinHu.Visualization.Plotter2D.Common
{
  public static class PlotterEvents
  {
    internal static void Notify(FrameworkElement target, PlotterChangedEventArgs args)
    {
      PlotterAttachedEvent.Notify(target, args);
      PlotterChangedEvent.Notify(target, args);
      PlotterDetachingEvent.Notify(target, args);
    }

    public static PlotterEventHelper PlotterAttachedEvent => new PlotterEventHelper(PlotterBase.PlotterAttachedEvent);

    public static PlotterEventHelper PlotterDetachingEvent => new PlotterEventHelper(PlotterBase.PlotterDetachingEvent);

    public static PlotterEventHelper PlotterChangedEvent => new PlotterEventHelper(PlotterBase.PlotterChangedEvent);
  }
}
