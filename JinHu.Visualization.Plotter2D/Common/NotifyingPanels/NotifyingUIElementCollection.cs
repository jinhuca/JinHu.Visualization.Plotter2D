using System;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;

namespace JinHu.Visualization.Plotter2D.Common
{
  internal sealed class NotifyingUIElementCollection : UIElementCollection, INotifyCollectionChanged
  {
    public NotifyingUIElementCollection(UIElement visualParent, FrameworkElement logicalParent)
      : base(visualParent, logicalParent)
    {
      Collection.CollectionChanged += OnCollectionChanged;
    }

    private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      CollectionChanged.Raise(this, e);
    }

    #region Overrides

    public override int Add(UIElement element)
    {
      Collection.Add(element);
      return base.Add(element);
    }

    public override void Clear()
    {
      Collection.Clear();
      base.Clear();
    }

    public override void Insert(int index, UIElement element)
    {
      Collection.Insert(index, element);
      base.Insert(index, element);
    }

    public override void Remove(UIElement element)
    {
      Collection.Remove(element);
      base.Remove(element);
    }

    public override void RemoveAt(int index)
    {
      Collection.RemoveAt(index);
      base.RemoveAt(index);
    }

    public override void RemoveRange(int index, int count)
    {
      for (int i = index; i < index + count; i++)
      {
        Collection.RemoveAt(i);
      }
      base.RemoveRange(index, count);
    }

    public override UIElement this[int index]
    {
      get { return base[index]; }
      set
      {
        Collection[index] = value;
        base[index] = value;
      }
    }

    public override int Count => Collection.Count;

    internal D3UIElementCollection Collection { get; } = new D3UIElementCollection();

    #endregion

    #region INotifyCollectionChanged Members

    public event NotifyCollectionChangedEventHandler CollectionChanged;

    #endregion
  }

  internal sealed class D3UIElementCollection : NotifiableCollection<UIElement>
  {
    protected override void OnItemAdding(UIElement item)
    {
      if (item == null)
      {
        throw new ArgumentNullException(nameof(item));
      }
    }
  }
}
