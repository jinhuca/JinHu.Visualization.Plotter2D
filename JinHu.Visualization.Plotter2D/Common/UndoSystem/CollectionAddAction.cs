using System;
using System.Collections.Generic;

namespace JinHu.Visualization.Plotter2D.Common
{
  public sealed class CollectionAddAction<T> : UndoAction
  {
    public CollectionAddAction(ICollection<T> collection, T item)
    {
      if (item == null)
      {
        throw new ArgumentNullException("addedItem");
      }

      Collection = collection ?? throw new ArgumentNullException("collection");
      Item = item;
    }

    public ICollection<T> Collection { get; }
    public T Item { get; }
    public override void Do() => Collection.Add(Item);
    public override void Undo() => Collection.Remove(Item);
  }
}
