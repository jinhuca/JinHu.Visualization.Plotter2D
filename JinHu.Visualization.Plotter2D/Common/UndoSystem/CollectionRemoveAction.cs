using System;
using System.Collections.Generic;

namespace JinHu.Visualization.Plotter2D.Common
{
  public sealed class CollectionRemoveAction<T> : UndoAction
  {
    public CollectionRemoveAction(IList<T> collection, T item, int index)
    {
      if (item == null)
      {
        throw new ArgumentNullException("addedItem");
      }

      Collection = collection ?? throw new ArgumentNullException("collection");
      Item = item;
      Index = index;
    }

    public IList<T> Collection { get; }
    public T Item { get; }
    public int Index { get; }
    public override void Do() => Collection.Remove(Item);
    public override void Undo() => Collection.Insert(Index, Item);
  }
}
