using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace JinHu.Visualization.Plotter2D
{
  /// <summary>
  ///   Composite point markers renders a specified set of markers at every point of graph.
  /// </summary>
  public sealed class CompositePointMarker : PointMarker
  {
    public CompositePointMarker() { }

    public CompositePointMarker(params PointMarker[] markers)
    {
      if (markers == null)
      {
        throw new ArgumentNullException("markers");
      }

      foreach (PointMarker m in markers)
      {
        Markers.Add(m);
      }
    }

    public CompositePointMarker(IEnumerable<PointMarker> markers)
    {
      if (markers == null)
      {
        throw new ArgumentNullException("markers");
      }

      foreach (PointMarker m in markers)
      {
        Markers.Add(m);
      }
    }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public Collection<PointMarker> Markers { get; } = new Collection<PointMarker>();

    public override void Render(DrawingContext dc, Point screenPoint)
    {
      LocalValueEnumerator enumerator = GetLocalValueEnumerator();
      foreach (var marker in Markers)
      {
        enumerator.Reset();
        while (enumerator.MoveNext())
        {
          marker.SetValue(enumerator.Current.Property, enumerator.Current.Value);
        }

        marker.Render(dc, screenPoint);
      }
    }
  }
}
