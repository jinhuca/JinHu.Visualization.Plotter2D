using JinHu.Visualization.Plotter2D;
using JinHu.Visualization.Plotter2D.DataSources;
using System;
using System.Data;
using System.Windows;
using System.Windows.Media;

namespace V0205DataTable2
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
      // (1) Prepare data
      var table = new DataTable();
      table.Columns.Add("Sine", typeof(double));
      table.Columns.Add("Time", typeof(DateTime));
      table.Columns.Add("Index", typeof(int));
      table.Columns.Add("Sqrt", typeof(double));

      for (int i = 0; i < 100; i++)
      {
        table.Rows.Add(Math.Sin(i / 10.0), DateTime.Now + new TimeSpan(0, 0, i), i, Math.Sqrt(i));
      }

      var data = new TableDataSource(table);

      //data.SetXMapping(row => ((DateTime)row["Time"] - (DateTime)table.Rows[0][1]).TotalSeconds);
      data.XMapping = row => ((DateTime) row["Time"] - (DateTime) table.Rows[0][1]).TotalSeconds;
      //data.SetYMapping(row=>10*(double)row["Sine"]);
      data.YMapping = row => 10 * (double) row["Sine"];
      data.AddMapping(ShapePointMarker.FillProperty, row => new SolidColorBrush(new HsbColor(3 * (int)row["Index"], 1, 1).ToArgbColor()));
      plotter.AddLineGraph(data, new Pen(Brushes.DarkGray, 1), new CirclePointMarker(){ Size = 10 }, new PenDescription("Sine"));

    }

  }
}
