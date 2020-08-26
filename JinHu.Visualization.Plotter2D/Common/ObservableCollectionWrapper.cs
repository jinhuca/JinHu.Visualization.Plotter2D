using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace JinHu.Visualization.Plotter2D.Common
{
  public class ObservableCollectionWrapper<T> : INotifyCollectionChanged, IList<T>
  {
    public ObservableCollectionWrapper() : this(new ObservableCollection<T>()) { }

    private readonly ObservableCollection<T> collection;
    public ObservableCollectionWrapper(ObservableCollection<T> _collection)
    {
      if (_collection == null)
      {
        throw new ArgumentNullException("collection");
      }

      collection = _collection;
      _collection.CollectionChanged += new NotifyCollectionChangedEventHandler(collection_CollectionChanged);
    }

    private int attemptsToRaiseChanged = 0;
    public bool RaisingEvents { get; private set; } = true;

    private void collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      attemptsToRaiseChanged++;
      if (RaisingEvents)
      {
        CollectionChanged.Raise(this, e);
      }
    }

    #region Update methods

    public void BeginUpdate()
    {
      attemptsToRaiseChanged = 0;
      RaisingEvents = false;
    }
    public void EndUpdate()
    {
      RaisingEvents = true;
      if (attemptsToRaiseChanged > 0)
      {
        CollectionChanged.Raise(this);
      }
    }

    public IDisposable BlockEvents() => new EventBlocker<T>(this);

    private sealed class EventBlocker<TT> : IDisposable
    {
      private readonly ObservableCollectionWrapper<TT> collection;
      public EventBlocker(ObservableCollectionWrapper<TT> _collection)
      {
        collection = _collection;
        _collection.BeginUpdate();
      }

      #region IDisposable Members

      public void Dispose() => collection.EndUpdate();

      #endregion
    }

    #endregion // end of Update methods

    #region IList<T> Members

    public int IndexOf(T item) => collection.IndexOf(item);

    public void Insert(int index, T item) => collection.Insert(index, item);

    public void RemoveAt(int index) => collection.RemoveAt(index);

    public T this[int index]
    {
      get => collection[index];
      set => collection[index] = value;
    }

    #endregion

    #region ICollection<T> Members

    public void Add(T item) => collection.Add(item);

    public void Clear() => collection.Clear();

    public bool Contains(T item) => collection.Contains(item);

    public void CopyTo(T[] array, int arrayIndex) => collection.CopyTo(array, arrayIndex);

    public int Count => collection.Count;

    public bool IsReadOnly => throw new NotImplementedException();

    public bool Remove(T item) => collection.Remove(item);

    #endregion

    #region IEnumerable<T> Members

    public IEnumerator<T> GetEnumerator() => collection.GetEnumerator();

    #endregion

    #region IEnumerable Members

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

    #endregion

    #region INotifyCollectionChanged Members

    public event NotifyCollectionChangedEventHandler CollectionChanged;

    #endregion
  }
}