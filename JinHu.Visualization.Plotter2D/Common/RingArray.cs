using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace JinHu.Visualization.Plotter2D.Common
{
  public class RingArray<T> : INotifyCollectionChanged, IList<T>
  {
    public RingArray(int capacity)
    {
      Capacity = capacity;
      array = new T[capacity];
    }

    public void Add(T item)
    {
      int index = (startIndex + Count) % Capacity;
      if (startIndex + Count >= Capacity)
      {
        startIndex++;
      }
      else
      {
        Count++;
      }

      array[index] = item;

      CollectionChanged.Raise(this);
    }

    public T this[int index]
    {
      get { return array[(startIndex + index) % Capacity]; }
      set
      {
        array[(startIndex + index) % Capacity] = value;
        CollectionChanged.Raise(this);
      }
    }

    public void Clear()
    {
      Count = 0;
      startIndex = 0;
      array = new T[Capacity];
    }

    public IEnumerator<T> GetEnumerator()
    {
      for (int i = 0; i < Count; i++)
      {
        yield return this[i];
      }
    }
    public int Count { get; private set; }
    private T[] array;
    public int Capacity { get; }
    private int startIndex = 0;

    #region INotifyCollectionChanged Members

    public event NotifyCollectionChangedEventHandler CollectionChanged;

    #endregion

    #region IList<T> Members

    public int IndexOf(T item)
    {
      int index = Array.IndexOf(array, item);

      if (index == -1)
      {
        return -1;
      }

      return (index - startIndex + Count) % Capacity;
    }

    public void Insert(int index, T item)
    {
      throw new NotImplementedException();
    }

    public void RemoveAt(int index)
    {
      throw new NotImplementedException();
    }

    #endregion

    #region ICollection<T> Members

    public bool Contains(T item)
    {
      return Array.IndexOf(array, item) > -1;
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
      throw new NotImplementedException();
    }

    public bool IsReadOnly
    {
      get { throw new NotImplementedException(); }
    }

    public bool Remove(T item)
    {
      throw new NotImplementedException();
    }

    #endregion

    #region IEnumerable Members

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    #endregion
  }
}
