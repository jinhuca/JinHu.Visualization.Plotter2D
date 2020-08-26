using System.Windows;
using System.Windows.Data;

namespace JinHu.Visualization.Plotter2D.Charts
{
  public static class LegendItemsHelper
  {
    public static LegendItem BuildDefaultLegendItem(IPlotterElement chart)
    {
      DependencyObject dependencyChart = (DependencyObject)chart;
      LegendItem result = new LegendItem();
      SetCommonBindings(result, chart);
      return result;
    }

    public static void SetCommonBindings(LegendItem legendItem, object chart)
    {
      legendItem.DataContext = chart;
      legendItem.SetBinding(Legend.VisualContentProperty, new Binding { Path = new PropertyPath("(0)", Legend.VisualContentProperty) });
      legendItem.SetBinding(Legend.DescriptionProperty, new Binding { Path = new PropertyPath("(0)", Legend.DescriptionProperty) });
    }
  }
}