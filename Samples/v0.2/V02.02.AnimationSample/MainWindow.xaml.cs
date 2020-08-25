using JinHu.Visualization.Plotter2D;
using JinHu.Visualization.Plotter2D.DataSources;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace V02.AnimationSample
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    double phase = 0;
    readonly double[] animatedX = new double[1000];
    readonly double[] animatedY = new double[1000];
    EnumerableDataSource<double> animatedDataSource = null;

    Header chartHeader = new Header();
    TextBlock headerContent = new TextBlock();

    readonly DispatcherTimer timer = new DispatcherTimer();

    public MainWindow()
    {
      InitializeComponent();
      InitializeChart();
    }

    private void InitializeChart()
    {
      headerContent = new TextBlock()
      {
        FontSize = 24,
        Text = "Phase = 0.00",
        HorizontalAlignment = HorizontalAlignment.Center
      };
      chartHeader.Content = headerContent;
      plotter.Children.Add(chartHeader);
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
      for (int i = 0; i < animatedX.Length; i++)
      {
        animatedX[i] = 2 * Math.PI * i / animatedX.Length;
        animatedY[i] = Math.Sin(animatedX[i]);
      }

      EnumerableDataSource<double> xSrc = new EnumerableDataSource<double>(animatedX);
      xSrc.SetXMapping(x => x);
      animatedDataSource = new EnumerableDataSource<double>(animatedY);
      animatedDataSource.SetYMapping(y => y);

      plotter.AddLineGraph(new CompositeDataSource(xSrc, animatedDataSource), new Pen(Brushes.Magenta, 3), new PenDescription("Sin(x + phase)"));

      timer.Interval = TimeSpan.FromMilliseconds(10);
      timer.Tick += AnimatedPlot_Timer;
      timer.IsEnabled = true;

      plotter.FitToView();
    }

    private void AnimatedPlot_Timer(object sender, EventArgs e)
    {
      phase += 0.01;
      if (phase > 2 * Math.PI)
      {
        phase -= 2 * Math.PI;
      }
      for (int i = 0; i < animatedX.Length; i++)
      {
        animatedY[i] = Math.Cos(animatedX[i] + phase);
      }

      animatedDataSource.RaiseDataChanged();
      headerContent.Text = string.Format(CultureInfo.InvariantCulture, "Phase = {0:N2}", phase);
    }
  }
}
