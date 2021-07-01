using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace JinHu.Visualization.Plotter2D.Charts
{
  public sealed class LongOperationsIndicator : IPlotterElement
  {
    public LongOperationsIndicator()
    {
      timer.Tick += timer_Tick;
    }

    private void timer_Tick(object sender, EventArgs e)
    {
      UpdateWaitIndicator();
      timer.Stop();
    }

    #region IPlotterElement Members

    void IPlotterElement.OnPlotterAttached(PlotterBase plotter)
    {
      this.plotter = plotter;
    }

    void IPlotterElement.OnPlotterDetaching(PlotterBase plotter)
    {
      this.plotter = null;
    }

    private PlotterBase plotter;
    PlotterBase IPlotterElement.Plotter
    {
      get { return plotter; }
    }

    #endregion

    #region LongOperationRunning

    public static void BeginLongOperation(DependencyObject obj)
    {
      obj.SetValue(dp: LongOperationRunningProperty, value: true);
    }

    public static void EndLongOperation(DependencyObject obj)
    {
      obj.SetValue(dp: LongOperationRunningProperty, value: false);
    }

    public static bool GetLongOperationRunning(DependencyObject obj)
    {
      return (bool)obj.GetValue(dp: LongOperationRunningProperty);
    }

    public static void SetLongOperationRunning(DependencyObject obj, bool value)
    {
      obj.SetValue(dp: LongOperationRunningProperty, value: value);
    }

    public static readonly DependencyProperty LongOperationRunningProperty = DependencyProperty.RegisterAttached(
      name: "LongOperationRunning",
      propertyType: typeof(bool),
      ownerType: typeof(LongOperationsIndicator),
      defaultMetadata: new FrameworkPropertyMetadata(defaultValue: false, propertyChangedCallback: OnLongOperationRunningChanged));

    private static void OnLongOperationRunningChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      IPlotterElement element = d as IPlotterElement;
      var plotter = element == null ? PlotterBase.GetPlotter(obj: d) : element.Plotter;

      if (plotter != null)
      {
        var indicator = plotter.Children.OfType<LongOperationsIndicator>().FirstOrDefault();
        indicator?.OnLongOperationRunningChanged(element: element, longOperationRunning: (bool)e.NewValue);
      }
    }

    readonly UIElement indicator = LoadIndicator();

    private static UIElement LoadIndicator()
    {
      var resources = (ResourceDictionary)Application.LoadComponent(resourceLocator: new Uri(uriString: Constants.NavigationResourceUri, uriKind: UriKind.Relative));
      UIElement indicator = (UIElement)resources[key: "Indicator"];
      return indicator;
    }

    private readonly DispatcherTimer timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(value: 100) };
    private int operationsCounter = 0;
    private void OnLongOperationRunningChanged(IPlotterElement element, bool longOperationRunning)
    {
      int change = longOperationRunning ? +1 : -1;
      operationsCounter += change;

      if (plotter == null)
      {
        return;
      }

      if (operationsCounter == 1)
      {
        timer.Start();
      }
      else if (operationsCounter == 0)
      {
        timer.Stop();
        UpdateWaitIndicator();
      }
    }

    private void UpdateWaitIndicator()
    {
      if (operationsCounter == 1)
      {
        if (!plotter.MainCanvas.Children.Contains(element: indicator))
        {
          plotter.MainCanvas.Children.Add(element: indicator);
        }
        plotter.Cursor = Cursors.Wait;
      }
      else if (operationsCounter == 0)
      {
        plotter.MainCanvas.Children.Remove(element: indicator);
        plotter.ClearValue(dp: FrameworkElement.CursorProperty);
      }
    }

    #endregion // end of LongOperationRunning
  }
}
