using System.Windows.Media;

namespace JinHu.Visualization.Plotter2D.Graphs
{
  public class MarkerGraph<T> : PointsGraphBase where T : PointMarker
  {
    public T Marker { get; set; }

    protected override void OnRenderCore(DrawingContext dc, RenderState state)
    {
      if (DataSource == null || Marker == null)
      {
        return;
      }
    }
  }
}
