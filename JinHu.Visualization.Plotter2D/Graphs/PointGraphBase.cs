﻿using JinHu.Visualization.Plotter2D.Charts;
using JinHu.Visualization.Plotter2D.DataSources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace JinHu.Visualization.Plotter2D.Graphs
{
  public abstract class PointsGraphBase : ViewportElement2D, IOneDimensionalChart
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="PointsGraphBase"/> class.
    /// </summary>
    protected PointsGraphBase()
    {
      Viewport2D.SetIsContentBoundsHost(obj: this, value: true);
    }

    #region DataSource

    public IPointDataSource DataSource
    {
      get => (IPointDataSource)GetValue(dp: DataSourceProperty);
      set => SetValue(dp: DataSourceProperty, value: value);
    }

    public static readonly DependencyProperty DataSourceProperty = DependencyProperty.Register(
      name: nameof(DataSource),
      propertyType: typeof(IPointDataSource),
      ownerType: typeof(PointsGraphBase),
      typeMetadata: new FrameworkPropertyMetadata { AffectsRender = true, DefaultValue = null, PropertyChangedCallback = OnDataSourceChangedCallback });

    private static void OnDataSourceChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var graph = (PointsGraphBase)d;
      if (e.NewValue != e.OldValue)
      {
        graph.DetachDataSource(source: e.OldValue as IPointDataSource);
        graph.AttachDataSource(source: e.NewValue as IPointDataSource);
      }
      graph.OnDataSourceChanged(args: e);
    }

    private void AttachDataSource(IPointDataSource source)
    {
      if (source != null)
      {
        source.DataChanged += OnDataChanged;
      }
    }

    private void DetachDataSource(IPointDataSource source)
    {
      if (source != null)
      {
        source.DataChanged -= OnDataChanged;
      }
    }

    private void OnDataChanged(object sender, EventArgs e) => OnDataChanged();

    protected virtual void OnDataChanged()
    {
      UpdateBounds(dataSource: DataSource);
      RaiseDataChanged();
      Update();
    }

    public event EventHandler DataChanged;
    private void RaiseDataChanged() => DataChanged?.Invoke(sender: this, e: EventArgs.Empty);

    protected virtual void OnDataSourceChanged(DependencyPropertyChangedEventArgs args)
    {
      var newDataSource = (IPointDataSource)args.NewValue;
      if (newDataSource != null)
      {
        UpdateBounds(dataSource: newDataSource);
      }
      Update();
    }

    private void UpdateBounds(IPointDataSource dataSource)
    {
      if (Plotter != null)
      {
        var transform = GetTransform();
        DataRect bounds = BoundsHelper.GetViewportBounds(dataPoints: dataSource.GetPoints(), transform: transform.DataTransform);
        Viewport2D.SetContentBounds(obj: this, value: bounds);
      }
    }

    #endregion DataSource

    #region DataTransform

    private DataTransform _dataTransform;
    public DataTransform DataTransform
    {
      get => _dataTransform;
      set
      {
        if (_dataTransform != value)
        {
          _dataTransform = value;
          Update();
        }
      }
    }

    protected CoordinateTransform GetTransform()
    {
      if (Plotter == null)
      {
        return null;
      }
      var transform = Plotter.Viewport.Transform;
      if (_dataTransform != null)
      {
        transform = transform.WithDataTransform(dataTransform: _dataTransform);
      }
      return transform;
    }

    #endregion

    #region VisiblePoints

    public ReadOnlyCollection<Point> VisiblePoints
    {
      get => GetVisiblePoints(obj: this);
      protected set => SetVisiblePoints(obj: this, value: value);
    }

    public static ReadOnlyCollection<Point> GetVisiblePoints(DependencyObject obj) => (ReadOnlyCollection<Point>)obj.GetValue(dp: VisiblePointsProperty);

    public static void SetVisiblePoints(DependencyObject obj, ReadOnlyCollection<Point> value) => obj.SetValue(dp: VisiblePointsProperty, value: value);

    public static readonly DependencyProperty VisiblePointsProperty = DependencyProperty.RegisterAttached(
      name: nameof(VisiblePoints),
      propertyType: typeof(ReadOnlyCollection<Point>),
      ownerType: typeof(PointsGraphBase),
      defaultMetadata: new FrameworkPropertyMetadata(defaultValue: null, propertyChangedCallback: OnVisiblePointsChanged));

    private static void OnVisiblePointsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      if (d is PointsGraphBase graph)
      {
        graph.RaiseVisiblePointsChanged();
      }
    }

    public event EventHandler VisiblePointsChanged;
    protected void RaiseVisiblePointsChanged() => VisiblePointsChanged?.Raise(sender: this);

    private bool _provideVisiblePoints;
    public bool ProvideVisiblePoints
    {
      get => _provideVisiblePoints;
      set
      {
        _provideVisiblePoints = value;
        UpdateCore();
      }
    }

    #endregion

    protected IEnumerable<Point> GetPoints() => DataSource.GetPoints(context: GetContext());

    private readonly DataSource2dContext _context = new DataSource2dContext();
    protected DependencyObject GetContext() => _context;
  }
}
