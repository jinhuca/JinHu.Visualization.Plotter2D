using System;
using System.Collections.Generic;
using System.Windows;

namespace JinHu.Visualization.Plotter2D.Common
{
  public sealed class PlotterEventHelper
  {
    private readonly RoutedEvent @event;
    internal PlotterEventHelper(RoutedEvent @event) => this.@event = @event;

    // todo use a weakReference here
    private readonly Dictionary<DependencyObject, EventHandler<PlotterChangedEventArgs>> handlers = new Dictionary<DependencyObject, EventHandler<PlotterChangedEventArgs>>();

    public void Subscribe(DependencyObject target, EventHandler<PlotterChangedEventArgs> handler)
    {
      if (target == null)
      {
        throw new ArgumentNullException("target");
      }

      if (handler == null)
      {
        throw new ArgumentNullException("handler");
      }

      handlers.Add(target, handler);
    }

    internal void Notify(FrameworkElement target, PlotterChangedEventArgs args)
    {
      if (args.RoutedEvent == @event && handlers.ContainsKey(target))
      {
        var handler = handlers[target];
        handler(target, args);
      }
    }
  }
}
