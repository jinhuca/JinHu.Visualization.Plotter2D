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
      const int N = 15000;
      double[] x = new double[N];
      double[] y = new double[N];
      for (var i = 0; i < N; i++)
      {
        x[i] = i * 0.001;
        y[i] = Math.Tanh(x[i]);
      }

      var yDataSource = new EnumerableDataSource<double>(y);
      yDataSource.SetYMapping(yy => yy);
      var xDataSource = new EnumerableDataSource<double>(x);
      xDataSource.SetXMapping(xx => xx);
      var cData = new CompositeDataSource(xDataSource, yDataSource);

      plotter.AddLineGraph(cData, Colors.DarkBlue, 1);
    }
  }
}
