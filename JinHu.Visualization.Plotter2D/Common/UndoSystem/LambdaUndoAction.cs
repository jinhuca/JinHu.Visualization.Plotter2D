using System;

namespace JinHu.Visualization.Plotter2D.Common
{
  public sealed class LambdaUndoAction : UndoAction
  {
    public LambdaUndoAction(Action doAction, Action undoAction)
    {
      DoAction = doAction ?? throw new ArgumentNullException("doHander");
      UndoAction = undoAction ?? throw new ArgumentNullException("undoAction");
    }

    public Action DoAction { get; }

    public Action UndoAction { get; }

    public override void Do() => DoAction();

    public override void Undo() => UndoAction();
  }
}
