using JinHu.Visualization.Plotter2D;
using JinHu.Visualization.Plotter2D.Charts;
using JinHu.Visualization.Plotter2D.DataSources;
using System;
using System.Windows;
using System.Windows.Media;

namespace V02.MarkerSample
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

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
      // Prepare data in arrays
      const int N = 210;
      double[] x = new double[N];
      double[] cs = new double[N];
      double[] sn = new double[N];

      for (int i = 0; i < N; i++)
      {
        x[i] = i * .1;
        cs[i] = Math.Sin(x[i]);
        sn[i] = Math.Cos(x[i]);
      }

      // Add data sources:
      // 3 partial data sources, containing each of arrays
      var snDataSource = new EnumerableDataSource<double>(sn);
      //snDataSource.SetYMapping(y => y);
      snDataSource.YMapping = y => y;

      var xDataSource = new EnumerableDataSource<double>(x);
      xDataSource.XMapping = lx => lx;
      //xDataSource.SetXMapping(lx => lx);

      var csDataSource = new EnumerableDataSource<double>(cs);
      csDataSource.YMapping = y => y;
      //csDataSource.SetYMapping(y => y);

      var csqDataSource = new EnumerableDataSource<double>(cs);
      //csqDataSource.SetYMapping(y => y * y);
      csqDataSource.YMapping = y => y * y;

      // 2 composite data sources and 2 charts respectively:
      //  creating composite data source
      CompositeDataSource compositeDataSource1 = new CompositeDataSource(xDataSource, csDataSource);
      // adding graph to plotter

      plotter.AddLineGraph(
        compositeDataSource1,
        new Pen(Brushes.Blue, .1),
        new CirclePointMarker { Size = 12, Fill = new SolidColorBrush(new Color() { R = 123, G = 25, B = 0, A = 255 }) },
        new PenDescription("Sin"));

      plotter.AddCursor(new CursorCoordinateGraph() { LineStroke = Brushes.Red, LineStrokeThickness = 0.5 });

      plotter.MainCanvas.Background = Brushes.Transparent;
      plotter.MainCanvas.Opacity = 1;

      // creating composite data source for cs values
      CompositeDataSource compositeDataSource2 = new CompositeDataSource(xDataSource, csDataSource);

      // Adding second graph to plotter
      plotter.AddLineGraph(compositeDataSource2,
          new Pen(Brushes.Blue, 3),
          new TrianglePointMarker { Size = 20, Fill = Brushes.Blue },
          new PenDescription("Cos"));

      // creating composite data source for cs^2 values
      CompositeDataSource compositeDataSource3 = new CompositeDataSource(xDataSource, csqDataSource);

      // Adding thirs graph to plotter
      Pen dashed = new Pen(Brushes.Magenta, 3);
      dashed.DashStyle = DashStyles.Dot;
      plotter.AddLineGraph(compositeDataSource3, dashed, new PenDescription("Cos^2"));

      // Force evertyhing plotted to be visible
      plotter.FitToView();
    }
  }
}
