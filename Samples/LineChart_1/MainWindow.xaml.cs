using JinHu.Visualization.Plotter2D;
using JinHu.Visualization.Plotter2D.DataSources;
using System;
using System.Windows;
using System.Windows.Media;

namespace LineChart_1
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
      // (1) Prepare data in arrays
      const int N = 40;
      double[] xA = new double[N];
      double[] yA = new double[N];

      for (int i = 0; i < N; i++)
      {
        xA[i] = i;
        yA[i] = 0.01*Math.Sin(i);
      }

      var xDataSource = new EnumerableDataSource<double>(xA);
      xDataSource.XMapping = x => x;
      //xDataSource.SetXMapping(x => x);

      var yDataSource = new EnumerableDataSource<double>(yA);
      yDataSource.YMapping = y => y;
      //yDataSource.SetYMapping(y => y);

      // (2) Composite data sources
      CompositeDataSource compositeDataSource1 = new CompositeDataSource(xDataSource, yDataSource);

      // (3) Create LineGraph
      plotter.AddLineGraph(
        compositeDataSource1, 
         

        new Pen(Brushes.Red, 1),
        //new CirclePointMarker { Size = 2, Fill = Brushes.Red },
        new PenDescription("Sin"));
    }
  }
}
