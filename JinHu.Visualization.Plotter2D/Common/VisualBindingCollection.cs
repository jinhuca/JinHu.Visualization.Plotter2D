using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;

namespace JinHu.Visualization.Plotter2D.Common
{
  [DebuggerDisplay("Count = {Cache.Count}")]
  public sealed class VisualBindingCollection
  {
    internal Dictionary<IPlotterElement, UIElement> Cache { get; } = new Dictionary<IPlotterElement, UIElement>();
    public UIElement this[IPlotterElement element] => Cache[element];
    public bool Contains(IPlotterElement element) => Cache.ContainsKey(element);
  }
}
