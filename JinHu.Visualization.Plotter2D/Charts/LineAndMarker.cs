using JinHu.Visualization.Plotter2D.Graphs;

namespace JinHu.Visualization.Plotter2D
{
  public sealed class LineAndMarker<T>
  {
    public LineGraph LineGraph { get; set; }
    public T MarkerGraph { get; set; }
  }
}
