using JinHu.Visualization.Plotter2D.Charts;
using JinHu.Visualization.Plotter2D.DataSources;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Media;

namespace JinHu.Visualization.Plotter2D
{
  /// <summary>
  ///   Extensions for <see cref="PlotterBase"/> - simplified methods to add line and marker charts.
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
    ///   Adds one dimensional graph with random color of line.
    /// </summary>
    /// <param name="pointSource">
    ///   The point source.
    /// </param>
    public static LineGraph AddLineGraph(this PlotterBase plotter, IPointDataSource pointSource)
      => AddLineGraph(plotter, pointSource, ColorHelper.CreateRandomHsbColor());

    /// <summary>
    ///   Adds one dimensional graph with specified color of line.
    /// </summary>
    /// <param name="pointSource">
    ///   The point source.
    /// </param>
    /// <param name="lineColor">
    ///   Color of the line.
    /// </param>
    /// <returns></returns>
    public static LineGraph AddLineGraph(this PlotterBase plotter, IPointDataSource pointSource, Color lineColor)
      => AddLineGraph(plotter, pointSource, lineColor, 1);

    /// <summary>
    ///   Adds one dimensional graph with random color if line.
    /// </summary>
    /// <param name="pointSource">
    ///   The point source.
    /// </param>
    /// <param name="lineThickness">
    ///   The line thickness.
    /// </param>
    /// <returns></returns>
    public static LineGraph AddLineGraph(this PlotterBase plotter, IPointDataSource pointSource, double lineThickness)
      => AddLineGraph(plotter, pointSource, ColorHelper.CreateRandomHsbColor(), lineThickness);

    /// <summary>
    ///   Adds one dimensional graph.
    /// </summary>
    /// <param name="pointSource">
    ///   The point source.
    /// </param>
    /// <param name="lineColor">
    ///   Color of the line.
    /// </param>
    /// <param name="lineThickness">
    ///   The line thickness.
    /// </param>
    /// <param name="description">
    ///   Description of data.
    /// </param>
    /// <returns></returns>
    public static LineGraph AddLineGraph(this PlotterBase plotter, IPointDataSource pointSource, Color lineColor, double lineThickness, string description)
      => AddLineGraph(plotter, pointSource, new Pen(new SolidColorBrush(lineColor), lineThickness), new PenDescription(description));

    /// <summary>
    ///   Adds one dimensional graph.
    /// </summary>
    /// <param name="pointSource">The point source.</param>
    /// <param name="lineColor">Color of the line.</param>
    /// <param name="lineThickness">The line thickness.</param>
    /// <returns></returns>
    public static LineGraph AddLineGraph(this PlotterBase plotter, IPointDataSource pointSource, Color lineColor, double lineThickness)
      => AddLineGraph(plotter, pointSource, new Pen(new SolidColorBrush(lineColor), lineThickness), null);

    /// <summary>
    /// Adds one dimensional graph.
    /// </summary>
    /// <param name="pointSource">The point source.</param>
    /// <param name="description">The description.</param>
    /// <returns></returns>
    public static LineGraph AddLineGraph(this PlotterBase plotter, IPointDataSource pointSource, string description)
    {
      LineGraph graph = AddLineGraph(plotter, pointSource);
      graph.Description = new PenDescription(description);
      Legend.SetDescription(graph, description);
      return graph;
    }

    /// <summary>
    /// Adds one dimensional graph.
    /// </summary>
    /// <param name="pointSource">The point source.</param>
    /// <param name="lineThickness">The line thickness.</param>
    /// <param name="description">The description.</param>
    /// <returns></returns>
    public static LineGraph AddLineGraph(this PlotterBase plotter, IPointDataSource pointSource, double lineThickness, string description)
    {
      var res = AddLineGraph(
        plotter,
        pointSource,
        new Pen(new SolidColorBrush(ColorHelper.CreateRandomHsbColor()), lineThickness),
        (PointMarker)null,
        new PenDescription(description));

      return res.LineGraph;
    }

    [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
    public static LineGraph AddLineGraph(this PlotterBase plotter, IPointDataSource pointSource, Pen linePen, Description description)
    {
      if (pointSource == null)
      {
        throw new ArgumentNullException(nameof(pointSource));
      }

      if (linePen == null)
      {
        throw new ArgumentNullException(nameof(linePen));
      }

      LineGraph graph = new LineGraph
      {
        DataSource = pointSource,
        LinePen = linePen
      };
      if (description != null)
      {
        Legend.SetDescription(graph, description.Brief);
        graph.Description = description;
      }
      // graph.Filters.Add(new InclinationFilter());
      graph.Filters.Add(new FrequencyFilter());
      plotter.Children.Add(graph);
      return graph;
    }

    #endregion [-- Line graphs --]

    #region [-- Point graphs --]

    /// <summary>
    /// Adds one dimensional graph to plotter. This method allows you to specify
    /// as much graph parameters as possible.
    /// </summary>
    /// <param name="pointSource">Source of points to plot</param>
    /// <param name="linePen">Pen to draw the line. If pen is null no lines will be drawn</param>
    /// <param name="marker">Marker to draw on points. If marker is null no points will be drawn</param>
    /// <param name="description">Description of graph to put in legend</param>
    /// <returns></returns>
    [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
    public static LineAndMarker<MarkerPointsGraph> AddLineGraph(
      this PlotterBase plotter,
      IPointDataSource pointSource,
      Pen linePen,
      PointMarker marker,
      Description description)
    {
      if (pointSource == null)
      {
        throw new ArgumentNullException(nameof(pointSource));
      }

      var res = new LineAndMarker<MarkerPointsGraph>();

      if (linePen != null) // We are requested to draw line graphs
      {
        LineGraph graph = new LineGraph
        {
          DataSource = pointSource,
          LinePen = linePen
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
    /// <param name="linePen">Pen to draw the line. If pen is null no lines will be drawn</param>
    /// <param name="marker">Marker to draw on points. If marker is null no points will be drawn</param>
    /// <param name="description">Description of graph to put in legend</param>
    /// <returns></returns>
    [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
    public static LineAndMarker<ElementMarkerPointsGraph> AddLineGraph(
      this PlotterBase plotter,
      IPointDataSource pointSource,
      Pen linePen,
      ElementPointMarker marker,
      Description description)
    {
      if (pointSource == null)
      {
        throw new ArgumentNullException("pointSource");
      }

      var res = new LineAndMarker<ElementMarkerPointsGraph>();

      if (linePen != null) // We are requested to draw line graphs
      {
        LineGraph graph = new LineGraph
        {
          DataSource = pointSource,
          LinePen = linePen
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

        graph.Filters.Add(new FrequencyFilter());

        res.LineGraph = graph;

        plotter.Children.Add(graph);
      }

      if (marker != null) // We are requested to draw marker graphs
      {
        ElementMarkerPointsGraph markerGraph = new ElementMarkerPointsGraph
        {
          DataSource = pointSource,
          Marker = marker
        };

        res.MarkerGraph = markerGraph;

        plotter.Children.Add(markerGraph);
      }

      return res;
    }

    #endregion [-- Point graphs --]

    #region Attaching LineGraphs

    public static void AttachLineGraph(this PlotterBase plotter, IPointDataSource pointSource, Pen linePen, PointMarker marker, Description desc)
    {
    }

    #endregion Attaching LineGraphs w/o Markers
  }
}
