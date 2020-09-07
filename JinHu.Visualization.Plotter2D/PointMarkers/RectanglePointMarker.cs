using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace JinHu.Visualization.Plotter2D.PointMarkers
{
  public class RectanglePointMarker : ShapePointMarker
  {
    public override void Render(DrawingContext dc, Point screenPoint)
    {
      var rec = new Rect(screenPoint, new Size(this.Size / 2, this.Size / 2));
      dc.DrawRectangle(Fill, Pen, rec);
    }
  }
}
