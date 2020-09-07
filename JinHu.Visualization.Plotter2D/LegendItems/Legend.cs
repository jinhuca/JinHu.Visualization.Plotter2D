using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Effects;
using System.Windows.Shapes;

namespace JinHu.Visualization.Plotter2D.Charts
{
  public delegate IEnumerable<FrameworkElement> LegendItemsBuilder(IPlotterElement element);

  public class Legend : ItemsControl, IPlotterElement
  {
    static Legend()
    {
      Type thisType = typeof(Legend);
      DefaultStyleKeyProperty.OverrideMetadata(thisType, new FrameworkPropertyMetadata(thisType));
      PlotterBase.PlotterProperty.OverrideMetadata(thisType, new FrameworkPropertyMetadata(OnPlotterChanged));
    }

    private readonly ObservableCollection<FrameworkElement> legendItems = new ObservableCollection<FrameworkElement>();

    public Legend() => ItemsSource = legendItems;

    #region IPlotterElement Members

    private static void OnPlotterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      Legend legend = (Legend)d;
      if (e.OldValue != null)
      {
        legend.DetachFromPlotter((PlotterBase)e.OldValue);
      }
      if (e.NewValue != null)
      {
        legend.AttachToPlotter((PlotterBase)e.NewValue);
      }
    }

    private PlotterBase plotter;
    public void OnPlotterAttached(PlotterBase plotter)
    {
      plotter.CentralGrid.Children.Add(this);
      AttachToPlotter(plotter);
    }

    private void AttachToPlotter(PlotterBase plotter)
    {
      if (plotter != this.plotter)
      {
        this.plotter = plotter;
        plotter.Children.CollectionChanged += PlotterChildren_CollectionChanged;
        PopulateLegend();
      }
    }

    public void OnPlotterDetaching(PlotterBase plotter)
    {
      plotter.CentralGrid.Children.Remove(this);
      DetachFromPlotter(plotter);
    }

    private void DetachFromPlotter(PlotterBase plotter)
    {
      if (plotter != null)
      {
        plotter.Children.CollectionChanged -= PlotterChildren_CollectionChanged;
        this.plotter = null;
        CleanLegend();
      }
    }

    public PlotterBase Plotter => plotter;

    #endregion

    private void PlotterChildren_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) => PopulateLegend();

    public void PopulateLegend()
    {
      legendItems.Clear();

      if (!LegendVisible)
      {
        return;
      }

      foreach (var chart in plotter.Children.OfType<DependencyObject>())
      {
        var legendItemsBuilder = GetLegendItemsBuilder(chart);
        if (legendItemsBuilder != null)
        {
          foreach (var legendItem in legendItemsBuilder((IPlotterElement)chart))
          {
            legendItems.Add(legendItem);
          }
        }

        //var controller = LegendItemControllersStore.Current.GetController(chart.GetType());
        //if (controller != null)
        //{
        //    controller.ProcessChart(chart, this);
        //}
      }

      UpdateVisibility();
    }

    private void UpdateVisibility()
    {
      Visibility = legendItems.Count > 0 ? Visibility.Visible : Visibility.Hidden;
    }

    private void CleanLegend()
    {
      foreach (var legendItem in legendItems)
      {
        BindingOperations.ClearAllBindings(legendItem);
      }
      legendItems.Clear();
      UpdateVisibility();
    }

    #region Attached Properties

    #region Description

    public static object GetDescription(DependencyObject obj)
    {
      return obj.GetValue(DescriptionProperty);
    }

    public static void SetDescription(DependencyObject obj, object value)
    {
      obj.SetValue(DescriptionProperty, value);
    }

    public static readonly DependencyProperty DescriptionProperty = DependencyProperty.RegisterAttached(
      "Description",
      typeof(object),
      typeof(Legend),
      new FrameworkPropertyMetadata(null));

    #endregion // end of Description

    #region Detailed description

    public static object GetDetailedDescription(DependencyObject obj)
    {
      return (object)obj.GetValue(DetailedDescriptionProperty);
    }

    public static void SetDetailedDescription(DependencyObject obj, object value)
    {
      obj.SetValue(DetailedDescriptionProperty, value);
    }

    public static readonly DependencyProperty DetailedDescriptionProperty = DependencyProperty.RegisterAttached(
      "DetailedDescription",
      typeof(object),
      typeof(Legend),
      new FrameworkPropertyMetadata(null));

    #endregion // end of Detailed description

    #region VisualContent

    public static object GetVisualContent(DependencyObject obj)
    {
      return (object)obj.GetValue(VisualContentProperty);
    }

    public static void SetVisualContent(DependencyObject obj, object value)
    {
      obj.SetValue(VisualContentProperty, value);
    }

    public static readonly DependencyProperty VisualContentProperty = DependencyProperty.RegisterAttached(
      "VisualContent",
      typeof(object),
      typeof(Legend),
      new FrameworkPropertyMetadata(null));

    #endregion // end of VisualContent

    #region SampleData

    public static object GetSampleData(DependencyObject obj)
    {
      return (object)obj.GetValue(SampleDataProperty);
    }

    public static void SetSampleData(DependencyObject obj, object value)
    {
      obj.SetValue(SampleDataProperty, value);
    }

    public static readonly DependencyProperty SampleDataProperty = DependencyProperty.RegisterAttached(
      "SampleData",
      typeof(object),
      typeof(Legend),
      new FrameworkPropertyMetadata(null));

    #endregion // end of SampleData

    #region ShowInLegend

    public static bool GetShowInLegend(DependencyObject obj)
    {
      return (bool)obj.GetValue(ShowInLegendProperty);
    }

    public static void SetShowInLegend(DependencyObject obj, bool value)
    {
      obj.SetValue(ShowInLegendProperty, value);
    }

    public static readonly DependencyProperty ShowInLegendProperty = DependencyProperty.RegisterAttached(
      "ShowInLegend",
      typeof(bool),
      typeof(Legend),
      new FrameworkPropertyMetadata(true, OnShowInLegendChanged));

    private static void OnShowInLegendChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      Legend legend = (Legend)d;
      legend.PopulateLegend();
    }

    #endregion // end of ShowInLegend

    #region LegendItemsBuilder

    public static LegendItemsBuilder GetLegendItemsBuilder(DependencyObject obj)
    {
      return (LegendItemsBuilder)obj.GetValue(LegendItemsBuilderProperty);
    }

    public static void SetLegendItemsBuilder(DependencyObject obj, LegendItemsBuilder value)
    {
      obj.SetValue(LegendItemsBuilderProperty, value);
    }

    public static readonly DependencyProperty LegendItemsBuilderProperty = DependencyProperty.RegisterAttached(
      "LegendItemsBuilder",
      typeof(LegendItemsBuilder),
      typeof(Legend),
      new FrameworkPropertyMetadata(null, OnLegendItemsBuilderChanged));

    private static void OnLegendItemsBuilderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      IPlotterElement plotterElement = d as IPlotterElement;
      if (plotterElement != null && plotterElement.Plotter != null)
      {
        Plotter plotter = plotterElement.Plotter as Plotter;
        if (plotter != null)
        {
          plotter.Legend.PopulateLegend();
        }
      }
    }

    #endregion // end of LegendItemsBuilder

    #endregion // end of Attached Properties

    #region Properties

    public bool LegendVisible
    {
      get { return (bool)GetValue(LegendVisibleProperty); }
      set { SetValue(LegendVisibleProperty, value); }
    }

    public static readonly DependencyProperty LegendVisibleProperty = DependencyProperty.Register(
      "LegendVisible",
      typeof(bool),
      typeof(Legend),
      new FrameworkPropertyMetadata(true, OnLegendVisibleChanged));

    private static void OnLegendVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      Legend owner = (Legend)d;
      var visible = (bool)e.NewValue;
      owner.OnLegendVisibleChanged(visible);
    }

    private void OnLegendVisibleChanged(bool visible)
    {
      Visibility = visible && legendItems.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
    }

    #endregion // end of Properties

    #region Overrides

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();

#if !RELEASEXBAP
      var rect = (Rectangle)Template.FindName("backRect", this);
      if (rect != null)
      {
        rect.Effect = new DropShadowEffect { Direction = 300, ShadowDepth = 3, Opacity = 0.4 };
      }
#endif
    }

    #endregion // end of Overrides
  }
}
