using System;

namespace JinHu.Visualization.Plotter2D.Common
{
  internal interface INotifyingPanel
  {
    NotifyingUIElementCollection NotifyingChildren { get; }
    event EventHandler ChildrenCreated;
  }
}
