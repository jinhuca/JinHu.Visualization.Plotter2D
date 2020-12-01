using JinHu.Visualization.Plotter2D.Charts;
using JinHu.Visualization.Plotter2D.DataSources;
using JinHu.Visualization.Plotter2D.Graphs;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Media;

namespace JinHu.Visualization.Plotter2D
{
  /// <summary>
  /// Extensions for <see cref="PlotterBase"/> - simplified methods to add line and marker charts.
  /// </summary>
  public static class PlotterBaseExtensions
  {
    #region [-- Cursor Coordinate Graphs --]

    public static void AddCursor(this PlotterBase plotter, CursorCoordinateGraph cursorGraph)
    {
      plotter.Children.Add(cursorGraph);
    }

    #endregion [-- Cursor Coordinate Graphs --]

    #region [-- Line graphs --]

    /// <summary>
    /// Extended method to add a LineGraph with a PointDataSource, and optional pen parameters.
    /// </summary>
    /// <param name="plotter">Host Plotter</param>
    /// <param name="pointSource">Data Source</param>
    /// <param name="penForDrawingLine">Optional OutlinePen</param>
    /// <param name="descriptionForPen">Optional descriptionForPen for OutlinePen</param>
    /// <returns>LineGraph</returns>
    public static LineGraph AddLineGraph(this PlotterBase plotter, IPointDataSource pointSource,
      Pen penForDrawingLine = default, PenDescription descriptionForPen = default)
    {
      if (pointSource == null)
      {
        throw new ArgumentNullException(nameof(pointSource));
      }

      penForDrawingLine ??= new Pen { Brush = new SolidColorBrush(Colors.Red), Thickness = 1 };
      descriptionForPen ??= new PenDescription(nameof(pointSource));

      var lineGraph = new LineGraph
      {
        DataSource = pointSource,
        LinePen = penForDrawingLine,
        Description = descriptionForPen
      };
      Legend.SetDescription(lineGraph, descriptionForPen.Brief);
      lineGraph.Filters.Add(new FrequencyFilter());
      plotter.Children.Add(lineGraph);
      return lineGraph;
    }

    #endregion [-- Line graphs --]

    #region [-- MarkerPointsGraph --]

    /// <summary>
    /// Extension method to add a MarkerPointPoint with a PointDataSource and parameters.
    /// </summary>
    /// <param name="plotter">Host Plotter</param>
    /// <param name="pointSource">Data Source</param>
    /// <param name="marker">Marker to add</param>
    /// <param name="description">Description</param>
    /// <returns></returns>
    public static MarkerPointsGraph AddMarkerPointsGraph(this PlotterBase plotter, IPointDataSource pointSource,
      PointMarker marker = default, Description description = default)
    {
      if (pointSource == null)
      {
        throw new ArgumentNullException(nameof(pointSource));
      }
      marker ??= new CirclePointMarker();
      var markerPointGraph = new MarkerPointsGraph { DataSource = pointSource, Marker = marker, Description = description };
      plotter.Children.Add(markerPointGraph);
      return markerPointGraph;
    }

    public static MarkerPointsGraph AddMarkerGraph<TMarker>(this PlotterBase plotter, IPointDataSource pointSource,
      TMarker marker = default, Description description = default) where TMarker : PointMarker
    {
      var res = new MarkerPointsGraph();
      switch (marker)
      {
        case CirclePointMarker s:
          break;
        case TrianglePointMarker t:
          break;
      }
      return res;
    }

    #endregion [-- MarkerPointsGraph --]

    #region [-- LineAndMarker graphs --]

    /// <summary>
    /// Adds one dimensional graph to plotter. This method allows you to specify
    /// as much graph parameters as possible.
    /// </summary>
    /// <param name="pointSource">Source of points to plot</param>
    /// <param name="penForDrawingLine">OutlinePen to draw the line. If pen is null no lines will be drawn</param>
    /// <param name="marker">Marker to draw on points. If marker is null no points will be drawn</param>
    /// <param name="description">Description of graph to put in legend</param>
    /// <returns></returns>
    [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
    public static LineAndMarker<MarkerPointsGraph> AddLineAndMarkerGraph(
      this PlotterBase plotter,
      IPointDataSource pointSource,
      Pen penForDrawingLine,
      PointMarker marker,
      Description description)
    {
      if (pointSource == null)
      {
        throw new ArgumentNullException(nameof(pointSource));
      }

      var res = new LineAndMarker<MarkerPointsGraph>();

      if (penForDrawingLine != null) // We are requested to draw line graphs
      {
        LineGraph graph = new LineGraph
        {
          DataSource = pointSource,
          LinePen = penForDrawingLine
        };
        if (description != null)
        {
          Legend.SetDescription(graph, description.Brief);
          graph.Description = description;
        }
        if (marker == null)
        {
          // Add inclination filter only to graphs without markers
          // graph.Filters.Add(new InclinationFilter());
        }

        res.LineGraph = graph;

        graph.Filters.Add(new FrequencyFilter());
        plotter.Children.Add(graph);
      }

      if (marker != null) // We are requested to draw marker graphs
      {
        MarkerPointsGraph markerGraph = new MarkerPointsGraph
        {
          DataSource = pointSource,
          Marker = marker
        };

        res.MarkerGraph = markerGraph;

        plotter.Children.Add(markerGraph);
      }

      return res;
    }

    /// <summary>
    /// Adds one dimensional graph to plotter. This method allows you to specify
    /// as much graph parameters as possible.
    /// </summary>
    /// <param name="pointSource">Source of points to plot</param>
    /// <param name="marker">Marker to draw on points. If marker is null no points will be drawn</param>
    /// <param name="description">Description of graph to put in legend</param>
    /// <returns></returns>
    [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
    public static ElementMarkerPointsGraph AddElementMarkerPointsGraph(
      this PlotterBase plotter,
      IPointDataSource pointSource,
      ElementPointMarker marker,
      Description description)
    {
      if (pointSource == null)
      {
        throw new ArgumentNullException(nameof(pointSource));
      }
      if (marker == null)
      {
        throw new ArgumentNullException(nameof(marker));
      }

      //if (penForDrawingLine != null) // We are requested to draw line graphs
      //{
      //  LineGraph graph = new LineGraph
      //  {
      //    DataSource = pointSource,
      //    LinePen = penForDrawingLine
      //  };
      //  if (description != null)
      //  {
      //    Legend.SetDescription(graph, description.Brief);
      //    graph.Description = description;
      //  }
      //  if (marker == null)
      //  {
      //    // Add inclination filter only to graphs without markers
      //    // graph.Filters.Add(new InclinationFilter());
      //  }

      //  graph.Filters.Add(new FrequencyFilter());

      //  res.LineGraph = graph;

      //  plotter.Children.Add(graph);
      //}

      var markerGraph = new ElementMarkerPointsGraph
      {
        DataSource = pointSource,
        Marker = marker,
        Description = description
      };
      plotter.Children.Add(markerGraph);
      return markerGraph;
    }

    #endregion [-- LineAndMarker graphs --]

    #region Attaching LineGraphs

    public static void AttachLineGraph(this PlotterBase plotter, IPointDataSource pointSource, Pen linePen, PointMarker marker, Description desc)
    {
    }

    #endregion Attaching LineGraphs w/o Markers
  }
}
