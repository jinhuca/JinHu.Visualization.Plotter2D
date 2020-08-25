using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace JinHu.Visualization.Plotter2D.Common
{
  [DebuggerDisplay("Count = {Count}")]
  public sealed class ActionStack
  {
    public void Push(UndoAction action)
    {
      Stack.Push(action);

      if (Stack.Count == 1)
      {
        RaiseIsEmptyChanged();
      }
    }

    public UndoAction Pop()
    {
      var action = Stack.Pop();

      if (Stack.Count == 0)
      {
        RaiseIsEmptyChanged();
      }

      return action;
    }

    public void Clear()
    {
      Stack.Clear();
      RaiseIsEmptyChanged();
    }

    public int Count => Stack.Count;
    public bool IsEmpty => Stack.Count == 0;
    public Stack<UndoAction> Stack { get; } = new Stack<UndoAction>();
    private void RaiseIsEmptyChanged() => IsEmptyChanged.Raise(this);
    public event EventHandler IsEmptyChanged;
  }
}
