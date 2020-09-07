using System;
using System.Windows;

namespace JinHu.Visualization.Plotter2D.Charts
{
  public static class LegendStyles
  {
    private static Style defaultStyle;
    public static Style Default
    {
      get
      {
        if (defaultStyle == null)
        {
          var legendStyles = GetLegendStyles();
          defaultStyle = (Style)legendStyles[typeof(Legend)];
        }

        return defaultStyle;
      }
    }

    private static Style noScrollStyle;
    public static Style NoScroll
    {
      get
      {
        if (noScrollStyle == null)
        {
          var legendStyles = GetLegendStyles();
          noScrollStyle = (Style)legendStyles["NoScrollLegendStyle"];
        }

        return noScrollStyle;
      }
    }

    private static ResourceDictionary GetLegendStyles()
    {
      var legendStyles = (ResourceDictionary)Application.LoadComponent(new Uri(Constants.LegendResourceUri, UriKind.Relative));
      return legendStyles;
    }
  }
}
