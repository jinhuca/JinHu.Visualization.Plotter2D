using System;
using System.Windows;
using System.Windows.Controls;

namespace JinHu.Visualization.Plotter2D.Common
{
  internal sealed class NotifyingStackPanel : StackPanel, INotifyingPanel
  {
    public NotifyingUIElementCollection NotifyingChildren { get; private set; }

    protected override UIElementCollection CreateUIElementCollection(FrameworkElement logicalParent)
    {
      NotifyingChildren = new NotifyingUIElementCollection(this, logicalParent);
      ChildrenCreated.Raise(this);
      return NotifyingChildren;
    }

    public event EventHandler ChildrenCreated;
    public override string ToString() => typeof(NotifyingStackPanel).Name + " Name: " + Name;
  }
}
