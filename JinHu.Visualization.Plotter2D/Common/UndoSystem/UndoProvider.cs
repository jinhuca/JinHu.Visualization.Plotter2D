using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace JinHu.Visualization.Plotter2D.Common
{
  public class UndoProvider : INotifyPropertyChanged
  {
    public UndoProvider()
    {
      UndoStack.IsEmptyChanged += OnUndoStackIsEmptyChanged;
      RedoStack.IsEmptyChanged += OnRedoStackIsEmptyChanged;
    }
    public bool IsEnabled { get; set; } = true;

    private void OnUndoStackIsEmptyChanged(object sender, EventArgs e)
    {
      PropertyChanged.Raise(this, "CanUndo");
    }

    private void OnRedoStackIsEmptyChanged(object sender, EventArgs e)
    {
      PropertyChanged.Raise(this, "CanRedo");
    }

    public void AddAction(UndoAction action)
    {
      if (!IsEnabled)
      {
        return;
      }

      if (State != UndoState.None)
      {
        return;
      }

      UndoStack.Push(action);
      RedoStack.Clear();
    }

    public void Undo()
    {
      var action = UndoStack.Pop();
      RedoStack.Push(action);

      State = UndoState.Undoing;
      try
      {
        action.Undo();
      }
      finally
      {
        State = UndoState.None;
      }
    }

    public void Redo()
    {
      var action = RedoStack.Pop();
      UndoStack.Push(action);

      State = UndoState.Redoing;
      try
      {
        action.Do();
      }
      finally
      {
        State = UndoState.None;
      }
    }

    public bool CanUndo => !UndoStack.IsEmpty;
    public bool CanRedo => !RedoStack.IsEmpty;
    public UndoState State { get; private set; } = UndoState.None;
    public ActionStack UndoStack { get; } = new ActionStack();
    public ActionStack RedoStack { get; } = new ActionStack();

    private Dictionary<CaptureKeyHolder, object> CaptureHolders { get; } = new Dictionary<CaptureKeyHolder, object>();

    #region INotifyPropertyChanged Members

    public event PropertyChangedEventHandler PropertyChanged;

    #endregion

    public void CaptureOldValue(DependencyObject target, DependencyProperty property, object oldValue)
    {
      CaptureHolders[new CaptureKeyHolder { Target = target, Property = property }] = oldValue;
    }

    public void CaptureNewValue(DependencyObject target, DependencyProperty property, object newValue)
    {
      var holder = new CaptureKeyHolder { Target = target, Property = property };
      if (CaptureHolders.ContainsKey(holder))
      {
        object oldValue = CaptureHolders[holder];
        CaptureHolders.Remove(holder);

        if (!object.Equals(oldValue, newValue))
        {
          var action = new DependencyPropertyChangedUndoAction(target, property, oldValue, newValue);
          AddAction(action);
        }
      }
    }

    private sealed class CaptureKeyHolder
    {
      public DependencyObject Target { get; set; }
      public DependencyProperty Property { get; set; }

      public override int GetHashCode()
      {
        return Target.GetHashCode() ^ Property.GetHashCode();
      }

      public override bool Equals(object obj)
      {
        if (obj == null)
        {
          return false;
        }

        CaptureKeyHolder other = obj as CaptureKeyHolder;
        if (other == null)
        {
          return false;
        }

        return Target == other.Target && Property == other.Property;
      }
    }
  }

  public enum UndoState
  {
    None,
    Undoing,
    Redoing
  }
}
