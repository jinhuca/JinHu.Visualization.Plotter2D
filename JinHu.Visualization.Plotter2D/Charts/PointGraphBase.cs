using JinHu.Visualization.Plotter2D.Charts;
using JinHu.Visualization.Plotter2D.DataSources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace JinHu.Visualization.Plotter2D
{
  public abstract class PointsGraphBase : ViewportElement2D, IOneDimensionalChart
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="PointsGraphBase"/> class.
    /// </summary>
    protected PointsGraphBase()
    {
      Viewport2D.SetIsContentBoundsHost(this, true);
    }

    #region DataSource

    public IPointDataSource DataSource
    {
      get => (IPointDataSource)GetValue(DataSourceProperty);
      set => SetValue(DataSourceProperty, value);
    }

    public static readonly DependencyProperty DataSourceProperty = DependencyProperty.Register(
      nameof(DataSource), typeof(IPointDataSource), typeof(PointsGraphBase),
      new FrameworkPropertyMetadata { AffectsRender = true, DefaultValue = null, PropertyChangedCallback = OnDataSourceChangedCallback });

    private static void OnDataSourceChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var graph = (PointsGraphBase)d;
      if (e.NewValue != e.OldValue)
      {
        graph.DetachDataSource(e.OldValue as IPointDataSource);
        graph.AttachDataSource(e.NewValue as IPointDataSource);
      }
      graph.OnDataSourceChanged(e);
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
      UpdateBounds(DataSource);
      RaiseDataChanged();
      Update();
    }

    public event EventHandler DataChanged;
    private void RaiseDataChanged() => DataChanged?.Invoke(this, EventArgs.Empty);

    protected virtual void OnDataSourceChanged(DependencyPropertyChangedEventArgs args)
    {
      IPointDataSource newDataSource = (IPointDataSource)args.NewValue;
      if (newDataSource != null)
      {
        UpdateBounds(newDataSource);
      }
      Update();
    }

    private void UpdateBounds(IPointDataSource dataSource)
    {
      if (Plotter != null)
      {
        var transform = GetTransform();
        DataRect bounds = BoundsHelper.GetViewportBounds(dataSource.GetPoints(), transform.DataTransform);
        Viewport2D.SetContentBounds(this, bounds);
      }
    }

    #endregion DataSource

    #region DataTransform

    private DataTransform dataTransform;
    public DataTransform DataTransform
    {
      get { return dataTransform; }
      set
      {
        if (dataTransform != value)
        {
          dataTransform = value;
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
      if (dataTransform != null)
      {
        transform = transform.WithDataTransform(dataTransform);
      }
      return transform;
    }

    #endregion

    #region VisiblePoints

    public ReadOnlyCollection<Point> VisiblePoints
    {
      get { return GetVisiblePoints(this); }
      protected set { SetVisiblePoints(this, value); }
    }

    public static ReadOnlyCollection<Point> GetVisiblePoints(DependencyObject obj)
    {
      return (ReadOnlyCollection<Point>)obj.GetValue(VisiblePointsProperty);
    }

    public static void SetVisiblePoints(DependencyObject obj, ReadOnlyCollection<Point> value)
    {
      obj.SetValue(VisiblePointsProperty, value);
    }

    public static readonly DependencyProperty VisiblePointsProperty = DependencyProperty.RegisterAttached(
      "VisiblePoints",
      typeof(ReadOnlyCollection<Point>),
      typeof(PointsGraphBase),
      new FrameworkPropertyMetadata(null, OnVisiblePointsChanged));

    private static void OnVisiblePointsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      PointsGraphBase graph = d as PointsGraphBase;
      if (graph != null)
      {
        graph.RaiseVisiblePointsChanged();
      }
    }

    public event EventHandler VisiblePointsChanged;
    protected void RaiseVisiblePointsChanged() => VisiblePointsChanged?.Raise(this);

    private bool provideVisiblePoints = false;
    public bool ProvideVisiblePoints
    {
      get { return provideVisiblePoints; }
      set
      {
        provideVisiblePoints = value;
        UpdateCore();
      }
    }

    #endregion

    protected IEnumerable<Point> GetPoints() => DataSource.GetPoints(GetContext());

    private readonly DataSource2dContext context = new DataSource2dContext();
    protected DependencyObject GetContext() => context;
  }
}
