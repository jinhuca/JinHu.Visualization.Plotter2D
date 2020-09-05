﻿using JinHu.Visualization.Plotter2D;
using JinHu.Visualization.Plotter2D.DataSources;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace Plotter2DSample1
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class AnimatedSampleWindow : Window
  {
    double phase = 0;
    readonly double[] animatedX = new double[1000];
    readonly double[] animatedY = new double[1000];
    EnumerableDataSource<double> animatedDataSource = null;

    /// <summary>
    /// Programmatically created header
    /// </summary>
    Header chartHeader = new Header();

    /// <summary>
    /// Text contents of header
    /// </summary>
    TextBlock headerContents = new TextBlock();

    /// <summary>
    /// Timer to animate data
    /// </summary>
    readonly DispatcherTimer timer = new DispatcherTimer();

    public AnimatedSampleWindow()
    {
      InitializeComponent();
      headerContents.FontSize = 24;
      headerContents.Text = "Phase = 0.00";
      headerContents.HorizontalAlignment = HorizontalAlignment.Center;
      chartHeader.Content = headerContents;
      plotter.Children.Add(chartHeader);
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
        animatedY[i] = Math.Sin(animatedX[i] + phase);
      }

      // Here it is - signal that data is updated
      animatedDataSource.RaiseDataChanged();
      headerContents.Text = string.Format(CultureInfo.InvariantCulture, "Phase = {0:N2}", phase);
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
      for (int i = 0; i < animatedX.Length; i++)
      {
        animatedX[i] = 2 * Math.PI * i / animatedX.Length;
        animatedY[i] = Math.Sin(animatedX[i]);
      }

      EnumerableDataSource<double> xSrc = new EnumerableDataSource<double>(animatedX);
      xSrc.XMapping = x => x;
      //xSrc.SetXMapping(x => x);
      animatedDataSource = new EnumerableDataSource<double>(animatedY);
      animatedDataSource.YMapping = y => y;
      //animatedDataSource.SetYMapping(y => y);

      // Adding graph to plotter
      plotter.AddLineGraph(
        new CompositeDataSource(xSrc, animatedDataSource),
        new Pen(Brushes.Red, 1),
        new PenDescription("Sin(x + phase)"));

      timer.Interval = TimeSpan.FromMilliseconds(10);
      timer.Tick += AnimatedPlot_Timer;
      timer.IsEnabled = true;

      // Force evertyhing plotted to be visible
      plotter.FitToView();
    }
  }
}
