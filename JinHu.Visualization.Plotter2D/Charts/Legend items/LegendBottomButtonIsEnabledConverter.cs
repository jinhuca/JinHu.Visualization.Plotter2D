﻿using System;
using System.Globalization;

namespace JinHu.Visualization.Plotter2D.Charts
{
  internal sealed class LegendBottomButtonIsEnabledConverter : ThreeValuesMultiConverter<double, double, double>
  {
    protected override object ConvertCore(double value1, double value2, double value3, Type targetType, object parameter, CultureInfo culture)
    {
      double extentHeight = value1;
      double viewportHeight = value2;
      double offset = value3;

      return viewportHeight < (extentHeight - offset);
    }
  }
}
