using JinHu.Visualization.Plotter2D.DataSources;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace JinHu.Visualization.Plotter2D.Graphs
{
  public class ElementMarkerPointsGraph : PointsGraphBase
  {
    /// <summary>
    ///   List with created but unused markers.
    /// </summary>
    private readonly List<UIElement> unused = new List<UIElement>();

    /// <summary>
    ///   Initializes a new instance of the <see cref="MarkerPointsGraph"/> class.
    /// </summary>
    public ElementMarkerPointsGraph()
    {
      ManualTranslate = true; // We'll handle translation by ourselves
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="MarkerPointsGraph"/> class.
    /// </summary>
    /// <param name="dataSource">
    ///   The data source.
    /// </param>
    public ElementMarkerPointsGraph(IPointDataSource dataSource) : this()
    {
      DataSource = dataSource;
    }

    Grid _grid;
    Canvas _canvas;

    protected override void OnPlotterAttached(PlotterBase plotter)
    {
      base.OnPlotterAttached(plotter: plotter);
      _grid = new Grid();
      _canvas = new Canvas { ClipToBounds = true };
      _grid.Children.Add(element: _canvas);
      Plotter.CentralGrid.Children.Add(element: _grid);
    }

    protected override void OnPlotterDetaching(PlotterBase plotter)
    {
      Plotter.CentralGrid.Children.Remove(element: _grid);
      _grid = null;
      _canvas = null;
      base.OnPlotterDetaching(plotter: plotter);
    }

    protected override void OnDataChanged()
    {
      //			if (canvas != null)
      //			{
      //                foreach(UIElement child in canvas.Children)
      //                    unused.Add(child);
      //				canvas.Children.Clear();
      //			}
      // todo почему так?
      base.OnDataChanged();
    }

    public ElementPointMarker Marker
    {
      get => (ElementPointMarker)GetValue(dp: MarkerProperty);
      set => SetValue(dp: MarkerProperty, value: value);
    }

    public static readonly DependencyProperty MarkerProperty = DependencyProperty.Register(
      name: nameof(Marker),
      propertyType: typeof(ElementPointMarker),
      ownerType: typeof(ElementMarkerPointsGraph),
      typeMetadata: new FrameworkPropertyMetadata { DefaultValue = null, AffectsRender = true });

    protected override void OnRenderCore(DrawingContext dc, RenderState state)
    {
      if (Marker == null)
      {
        return;
      }

      if (DataSource == null) // No data is specified
      {
        if (_canvas != null)
        {
          foreach (UIElement child in _canvas.Children)
          {
            unused.Add(item: child);
          }
          _canvas.Children.Clear();
        }
      }
      else // There is some data
      {
        int index = 0;
        var transform = GetTransform();
        using IPointEnumerator enumerator = DataSource.GetEnumerator(context: GetContext());
        Point point = new Point();
        DataRect bounds = DataRect.Empty;

        while (enumerator.MoveNext())
        {
          enumerator.GetCurrent(p: ref point);
          enumerator.ApplyMappings(target: Marker);

          if (index >= _canvas.Children.Count)
          {
            UIElement newMarker;
            if (unused.Count > 0)
            {
              newMarker = unused[index: unused.Count - 1];
              unused.RemoveAt(index: unused.Count - 1);
            }
            else
            {
              newMarker = Marker.CreateMarker();
            }
            _canvas.Children.Add(element: newMarker);
          }

          Marker.SetMarkerProperties(marker: _canvas.Children[index: index]);
          bounds.Union(point: point);
          Point screenPoint = point.DataToScreen(transform: transform);
          Marker.SetPosition(marker: _canvas.Children[index: index], screenPoint: screenPoint);
          index++;
        }

        Viewport2D.SetContentBounds(obj: this, value: bounds);

        while (index < _canvas.Children.Count)
        {
          unused.Add(item: _canvas.Children[index: index]);
          _canvas.Children.RemoveAt(index: index);
        }
      }
    }
  }
}