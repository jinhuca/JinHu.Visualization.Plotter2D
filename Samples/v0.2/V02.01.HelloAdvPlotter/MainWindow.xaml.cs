using JinHu.Visualization.Plotter2D;
using JinHu.Visualization.Plotter2D.DataSources;
using System;
using System.Windows;
using System.Windows.Media;

namespace V02.HelloAdvPlotter
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
      Loaded += MainWindow_Loaded;
    }

    private void MainWindow_Loaded(object sender, RoutedEventArgs e) => LoadDataSource();

    private void LoadDataSource()
    {
      const int N = 10100;
      double[] x = new double[N];
      double[] y = new double[N];
      for (var i = 0; i < N; i++)
      {
        x[i] = i * 0.001;
        y[i] = Math.Sin(x[i]);
      }

      var xDataSource = new EnumerableDataSource<double>(x) { XMapping = xx => xx };
      var yDataSource = new EnumerableDataSource<double>(y) { YMapping = yy => yy };
      var composedData = new CompositeDataSource(xDataSource, yDataSource);

      plotter.AddLineGraph(pointSource: composedData, penForDrawingLine: new Pen(Brushes.Aqua, 1), descriptionForPen: new PenDescription("sin"));
    }
  }
}
