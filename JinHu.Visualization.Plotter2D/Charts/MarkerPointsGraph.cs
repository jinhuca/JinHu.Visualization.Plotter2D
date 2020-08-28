﻿using JinHu.Visualization.Plotter2D.DataSources;
using System.Windows;
using System.Windows.Media;

namespace JinHu.Visualization.Plotter2D
{
  public class MarkerPointsGraph : PointsGraphBase
  {
    /// <summary>
    ///   Initializes a new instance of the <see cref="MarkerPointsGraph"/> class.
    /// </summary>
    public MarkerPointsGraph()
    {
      ManualTranslate = true;
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="MarkerPointsGraph"/> class.
    /// </summary>
    /// <param name="dataSource">
    ///   The data source.
    /// </param>
    public MarkerPointsGraph(IPointDataSource dataSource) : this()
    {
      DataSource = dataSource;
    }

    protected override void OnVisibleChanged(DataRect newRect, DataRect oldRect)
    {
      base.OnVisibleChanged(newRect, oldRect);
      InvalidateVisual();
    }

    public PointMarker Marker
    {
      get { return (PointMarker)GetValue(MarkerProperty); }
      set { SetValue(MarkerProperty, value); }
    }

    public static readonly DependencyProperty MarkerProperty = DependencyProperty.Register(
      "Marker",
      typeof(PointMarker),
      typeof(MarkerPointsGraph),
      new FrameworkPropertyMetadata { DefaultValue = null, AffectsRender = true });

    protected override void OnRenderCore(DrawingContext dc, RenderState state)
    {
      if (DataSource == null)
      {
        return;
      }

      if (Marker == null)
      {
        return;
      }

      var transform = Plotter.Viewport.Transform;
      DataRect bounds = DataRect.Empty;
      using (IPointEnumerator enumerator = DataSource.GetEnumerator(GetContext()))
      {
        Point point = new Point();
        while (enumerator.MoveNext())
        {
          enumerator.GetCurrent(ref point);
          enumerator.ApplyMappings(Marker);

          //Point screenPoint = point.Transform(state.Visible, state.Output);
          Point screenPoint = point.DataToScreen(transform);

          bounds = DataRect.Union(bounds, point);
          Marker.Render(dc, screenPoint);
        }
      }

      Viewport2D.SetContentBounds(this, bounds);
    }
  }
}
