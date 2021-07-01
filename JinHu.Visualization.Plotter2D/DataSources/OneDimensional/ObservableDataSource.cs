using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Threading;

namespace JinHu.Visualization.Plotter2D.DataSources
{
  // todo I don't think that we should create data source which supports 
  // suspending its DataChanged event - it is better to create
  // collection with the same functionality - then it would be able to be used
  // as a source in many data sources.
  public class ObservableDataSource<T> : IPointDataSource
  {
    public ObservableDataSource()
    {
      Collection.CollectionChanged += OnCollectionChanged;

      // todo this is hack
      if (typeof(T) == typeof(Point))
      {
        XyMapping = t => (Point)(object)t;
      }
    }

    public ObservableDataSource(IEnumerable<T> data) : this()
    {
      if (data == null)
      {
        throw new ArgumentNullException(nameof(data));
      }
      foreach (T item in data)
      {
        Collection.Add(item);
      }
    }

    public void SuspendUpdate() => UpdatesEnabled = false;

    public void ResumeUpdate()
    {
      UpdatesEnabled = true;
      if (CollectionChanged)
      {
        CollectionChanged = false;
        RaiseDataChanged();
      }
    }

    private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      if (UpdatesEnabled)
      {
        RaiseDataChanged();
      }
      else
      {
        CollectionChanged = true;
      }
    }

    public ObservableCollection<T> Collection { get; } = new ObservableCollection<T>();
    public bool CollectionChanged { get; set; } = false;
    public bool UpdatesEnabled { get; set; } = true;

    internal List<Mapping<T>> Mappings { get; } = new List<Mapping<T>>();
    public Func<T, double> XMapping { get; set; }
    public Func<T, double> YMapping { get; set; }
    public Func<T, Point> XyMapping { get; set; }

    public void AppendMany(IEnumerable<T> data)
    {
      if (data == null)
      {
        throw new ArgumentNullException(nameof(data));
      }
      UpdatesEnabled = false;
      foreach (var p in data)
      {
        Collection.Add(p);
      }
      UpdatesEnabled = true;
      RaiseDataChanged();
    }

    public void AppendAsync(Dispatcher dispatcher, T item)
    {
      dispatcher.Invoke(DispatcherPriority.Normal,
        new Action(() =>
        {
          Collection.Add(item);
          RaiseDataChanged();
        }));
    }

    public void SetXMapping(Func<T, double> mapping) => XMapping = mapping ?? throw new ArgumentNullException(nameof(mapping));
    public void SetYMapping(Func<T, double> mapping) => YMapping = mapping ?? throw new ArgumentNullException(nameof(mapping));
    public void SetXYMapping(Func<T, Point> mapping) => XyMapping = mapping ?? throw new ArgumentNullException(nameof(mapping));

    #region IChartDataSource Members

    private class ObservableIterator : IPointEnumerator
    {
	    private ObservableDataSource<T> DataSource { get; }

	    private IEnumerator<T> Enumerator { get; }

      public ObservableIterator(ObservableDataSource<T> dataSource)
      {
        DataSource = dataSource;
        Enumerator = dataSource.Collection.GetEnumerator();
      }

      #region IChartPointEnumerator Members

      public bool MoveNext() => Enumerator.MoveNext();

      public void GetCurrent(ref Point p) => DataSource.FillPoint(Enumerator.Current, ref p);

      public void ApplyMappings(DependencyObject target) => DataSource.ApplyMappings(target, Enumerator.Current);

      public void Dispose()
      {
        Enumerator.Dispose();
        GC.SuppressFinalize(this);
      }

      #endregion
    }

    private void FillPoint(T elem, ref Point point)
    {
      if (XyMapping != null)
      {
        point = XyMapping(elem);
      }
      else
      {
        if (XMapping != null)
        {
          point.X = XMapping(elem);
        }
        if (YMapping != null)
        {
          point.Y = YMapping(elem);
        }
      }
    }

    private void ApplyMappings(DependencyObject target, T elem)
    {
      if (target != null)
      {
        foreach (var mapping in Mappings)
        {
          target.SetValue(mapping.Property, mapping.F(elem));
        }
      }
    }

    public IPointEnumerator GetEnumerator(DependencyObject context)
    {
      return new ObservableIterator(this);
    }

    public event EventHandler DataChanged;
    private void RaiseDataChanged() => DataChanged?.Invoke(this, EventArgs.Empty);

    #endregion
  }
}