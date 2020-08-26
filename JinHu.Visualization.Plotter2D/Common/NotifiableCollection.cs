using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace JinHu.Visualization.Plotter2D.Common
{
  /// <summary>
  ///   This is a base class for some of collections in JinHu.Visualization.Plotter2D assembly.
  ///   It provides means to be notified when item adding and added events, which enables successors to, for example,
  ///   check if adding item is not equal to null.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public abstract class NotifiableCollection<T> : ObservableCollection<T>
  {
    #region Overrides

    protected override void InsertItem(int index, T item)
    {
      OnItemAdding(item);
      base.InsertItem(index, item);
      OnItemAdded(item);
    }

    protected override void ClearItems()
    {
      foreach (var item in Items)
      {
        OnItemRemoving(item);
      }
      base.ClearItems();
    }

    protected override void RemoveItem(int index)
    {
      T item = Items[index];
      OnItemRemoving(item);
      base.RemoveItem(index);
    }

    protected override void SetItem(int index, T item)
    {
      T oldItem = Items[index];
      OnItemRemoving(oldItem);
      OnItemAdding(item);
      base.SetItem(index, item);
      OnItemAdded(item);
    }

    protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
    {
      attemptsToRaiseEvent++;
      if (raiseCollectionChangedEvent)
      {
        base.OnCollectionChanged(e);
      }
    }

    #endregion

    /// <summary>
    ///   Called before item added to collection. Enables to perform validation.
    /// </summary>
    /// <param name="item">
    ///   The adding item.
    /// </param>
    protected virtual void OnItemAdding(T item) { }

    /// <summary>
    ///   Called when item is added.
    /// </summary>
    /// <param name="item">
    ///   The added item.
    /// </param>
    protected virtual void OnItemAdded(T item) { }

    /// <summary>
    ///   Called when item is being removed, but before it is actually removed.
    /// </summary>
    /// <param name="item">
    ///   The removing item.
    /// </param>
    protected virtual void OnItemRemoving(T item) { }

    int attemptsToRaiseEvent = 0;
    bool raiseCollectionChangedEvent = true;

    #region Public

    public void BeginUpdate()
    {
      attemptsToRaiseEvent = 0;
      raiseCollectionChangedEvent = false;
    }

    public void EndUpdate(bool raiseReset)
    {
      raiseCollectionChangedEvent = true;
      if (attemptsToRaiseEvent > 0 && raiseReset)
      {
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
      }
    }

    public IDisposable BlockEvents(bool raiseReset) => new EventBlocker<T>(this, raiseReset);

    private sealed class EventBlocker<TT> : IDisposable
    {
      private readonly NotifiableCollection<TT> collection;
      private readonly bool raiseReset = true;

      public EventBlocker(NotifiableCollection<TT> _collection, bool _raiseReset)
      {
        collection = _collection;
        raiseReset = _raiseReset;
        _collection.BeginUpdate();
      }

      #region IDisposable Members

      public void Dispose() => collection.EndUpdate(raiseReset);

      #endregion
    }

    #endregion
  }
}
