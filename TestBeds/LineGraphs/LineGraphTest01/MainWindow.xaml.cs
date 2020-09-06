using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using JinHu.Visualization.Plotter2D;
using JinHu.Visualization.Plotter2D.DataSources;

namespace LineGraphTest01
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    private CompositeDataSource source = new CompositeDataSource();

    public MainWindow()
    {
      InitializeComponent();
      Loaded += MainWindow_Loaded;
    }

    private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
      source = await CreateDataSource();
      plotter.AddLineGraph(pointSource: source,
        penForDrawingLine: new Pen(brush: Brushes.DarkRed, thickness: 2),
        descriptionForPen: new PenDescription(nameof(Math.Sign)));
    }

    private async Task<CompositeDataSource> CreateDataSource()
    {
      return await Task.Run(() =>
      {
        const int N = 10_000;
        double[] xs = new double[N];
        double[] ys = new double[N];
        for (int i = 0; i < N; ++i)
        {
          xs[i] = i * 0.001;
          ys[i] = Math.Sin(xs[i]);
        }

        var xSource = new EnumerableDataSource<double>(xs) { XMapping = x => x };
        var ySource = new EnumerableDataSource<double>(ys) { YMapping = y => y };
        return new CompositeDataSource(xSource, ySource);
      });
    }
  }
}
