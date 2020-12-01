using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows;

namespace JinHu.Visualization.Plotter2D.DataSources
{
  /// <summary>
  /// Base class for all sources who receive data for charting from any IEnumerable of T
  /// </summary>
  /// <typeparam name="T">
  /// Type of items in IEnumerable.
  /// </typeparam>
  public abstract class EnumerableDataSourceBase<T> : IPointDataSource
  {
    #region Property Data

    private IEnumerable data;

    public IEnumerable Data
    {
      get => data;
      set
      {
        data = value ?? throw new ArgumentNullException(nameof(value));
        if (data is INotifyCollectionChanged observableCollection)
        {
          observableCollection.CollectionChanged += ObservableCollection_CollectionChanged;
        }
      }
    }
    private void ObservableCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) => RaiseDataChanged();

    #endregion Property Data

    #region Constructors

    protected EnumerableDataSourceBase(IEnumerable data) => Data = data ?? throw new ArgumentNullException(nameof(data));

    protected EnumerableDataSourceBase(IEnumerable<T> data) : this((IEnumerable)data) { }

    #endregion Constructors

    #region IPointDataSource implementation

    public event EventHandler DataChanged;

    public void RaiseDataChanged() => DataChanged?.Invoke(this, EventArgs.Empty);

    public abstract IPointEnumerator GetEnumerator(DependencyObject context);

    #endregion IPointDataSource implementation
  }
}