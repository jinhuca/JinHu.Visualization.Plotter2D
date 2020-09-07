using System;
using System.Data;
using System.Windows;
using System.Windows.Media;
using JinHu.Visualization.Plotter2D;
using JinHu.Visualization.Plotter2D.Charts;
using JinHu.Visualization.Plotter2D.DataSources;
using JinHu.Visualization.Plotter2D.PointMarkers;

namespace MarkerGraphTest01
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
    }

    private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
      var table = new DataTable();
      table.Columns.Add("Sine", typeof(double));
      table.Columns.Add("Time", typeof(DateTime));
      table.Columns.Add("Index", typeof(int));
      table.Columns.Add("Sqrt", typeof(double));
      table.Columns.Add("Cosine", typeof(double));

      for (int i = 0; i < 1000; i++)
      {
        table.Rows.Add(
          Math.Sin(i / 100.0),
          DateTime.Now + new TimeSpan(0, 0, i),
          i,
          Math.Sqrt(i / 100.0),
          Math.Cos(i / 100.0));
      }

      var data1 = new TableDataSource(table)
      {
        XMapping = row => ((DateTime)row["Time"] - (DateTime)table.Rows[0][1]).TotalSeconds,
        YMapping = row => 10 * (double)row["Sine"]
      };

      // Map HSB color computes from "Index" column to dependency property Brush of marker
      data1.AddMapping(ShapePointMarker.FillProperty, row => new SolidColorBrush(new HsbColor(15 * (int)row["Index"], 1, 1).ToArgbColor()));

      // Map "Sqrt" based values to marker size
      data1.AddMapping(ShapePointMarker.SizeProperty, row => 3 * (double)row["Sqrt"]);

      // Plot first graph
      plotter.AddMarkerPointsGraph(data1);

      // Plot second graph
      var data2 = new TableDataSource(table)
      {
        XMapping = row => ((DateTime)row["Time"] - (DateTime)table.Rows[0][1]).TotalSeconds,
        YMapping = row => 10 * (double)row["Cosine"]
      };

      data2.AddMapping(ShapePointMarker.FillProperty, row => new SolidColorBrush(new HsbColor(15 * (int)row["Index"], 1, 1).ToArgbColor()));
      data2.AddMapping(ShapePointMarker.SizeProperty, row => 3 * (double)row["Sqrt"]);

      var circleMarker = new CirclePointMarker()
      {
        Pen = new Pen {Brush = new SolidColorBrush(Colors.Black)}
      };

      var triangleMarker = new TrianglePointMarker()
      {
        Pen = new Pen {Brush = new SolidColorBrush(Colors.Black)}
      };
      triangleMarker.Pen.Freeze();

      var rectangleMarker = new RectanglePointMarker()
      {
        Pen = new Pen {Brush = new SolidColorBrush(Colors.Black)}
      };

      plotter.AddMarkerPointsGraph(pointSource: data2, triangleMarker);

      plotter.AddCursor(new CursorCoordinateGraph());
    }
  }
}
