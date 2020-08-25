using System.Windows;

namespace JinHu.Visualization.Plotter2D.Common
{
  public class DependencyPropertyChangedUndoAction : UndoAction
  {
    public DependencyProperty Property { get; }
    public DependencyObject Target { get; }
    public object OldValue { get; }
    public object NewValue { get; }

    public DependencyPropertyChangedUndoAction(DependencyObject target, DependencyProperty property, object oldValue, object newValue)
    {
      Target = target;
      Property = property;
      OldValue = oldValue;
      NewValue = newValue;
    }

    public DependencyPropertyChangedUndoAction(DependencyObject target, DependencyPropertyChangedEventArgs e)
    {
      Target = target;
      Property = e.Property;
      OldValue = e.OldValue;
      NewValue = e.NewValue;
    }

    public override void Do() => Target.SetValue(Property, NewValue);

    public override void Undo() => Target.SetValue(Property, OldValue);
  }
}
