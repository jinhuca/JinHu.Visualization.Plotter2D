﻿using System;

namespace JinHu.Visualization.Plotter2D.Charts
{
  internal sealed class LegendTopButtonToIsEnabledConverter : GenericValueConverter<double>
  {
    public override object ConvertCore(double value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      double verticalOffset = value;
      return verticalOffset > 0;
    }
  }
}
