using JinHu.Visualization.Plotter2D.Common;
using System;
using System.Collections.Specialized;

namespace JinHu.Visualization.Plotter2D
{
  /// <summary>
  ///   Represents a collection of <see cref="ViewportConstraint"/>s.
  /// </summary>
  /// <remarks>
  ///   ViewportConstraint that is being added should not be null.
  /// </remarks>
  public sealed class ConstraintCollection : NotifiableCollection<ViewportConstraint>
  {
    public Viewport2D Viewport { get; }

    internal ConstraintCollection(Viewport2D viewport)
    {
      Viewport = viewport ?? throw new ArgumentNullException("viewport");
    }

    protected override void OnItemAdding(ViewportConstraint item)
    {
      if (item == null)
      {
        throw new ArgumentNullException("item");
      }
    }

    protected override void OnItemAdded(ViewportConstraint item)
    {
      item.Changed += OnItemChanged;
      if (item is ISupportAttachToViewport attachable)
      {
        attachable.Attach(Viewport);
      }
    }

    private void OnItemChanged(object sender, EventArgs e)
    {
      OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }

    protected override void OnItemRemoving(ViewportConstraint item)
    {
      if (item is ISupportAttachToViewport attachable)
      {
        attachable.Detach(Viewport);
      }
      item.Changed -= OnItemChanged;
    }

    internal DataRect Apply(DataRect oldVisible, DataRect newVisible, Viewport2D viewport)
    {
      DataRect res = newVisible;
      foreach (var constraint in this)
      {
        res = constraint.Apply(oldVisible, res, viewport);
      }
      return res;
    }
  }
}
