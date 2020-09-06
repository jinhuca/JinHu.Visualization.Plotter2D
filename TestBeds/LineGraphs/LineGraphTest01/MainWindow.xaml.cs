using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using JinHu.Visualization.Plotter2D;
using JinHu.Visualization.Plotter2D.DataSources;

namespace LineGraphTest01
{
  /// <summary>
  /// Test basic operations: adding LineGraphs to Plotter.
  /// </summary>
  public partial class MainWindow : Window
  {
    private CompositeDataSource _source = new CompositeDataSource();

    public MainWindow()
    {
      InitializeComponent();
      Loaded += MainWindow_Loaded;
    }

    private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
      _source = await CreateDataSource();
      plotter.AddLineGraph(
        pointSource: _source,
        penForDrawingLine: new Pen(brush: Brushes.GreenYellow, thickness: 3),
        descriptionForPen: new PenDescription(nameof(Math.Sign)));
    }

    private async Task<CompositeDataSource> CreateDataSource()
    {
      const int N = 100_000;
      return await Task.Run(() =>
      {
        double[] xs = new double[N];
        double[] ys = new double[N];
        for (int i = 0; i < N; ++i)
        {
          xs[i] = i * 0.0001;
          ys[i] = Math.Sin(xs[i]);
        }

        var xSource = new EnumerableDataSource<double>(xs) { XMapping = x => x };
        var ySource = new EnumerableDataSource<double>(ys) { YMapping = y => y };
        return new CompositeDataSource(xSource, ySource);
      });
    }
  }
}
