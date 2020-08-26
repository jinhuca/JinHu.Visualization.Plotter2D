using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace JinHu.Visualization.Plotter2D
{
  public sealed class BrushHSBConverter : IValueConverter
  {
    public double LightnessDelta { get; set; } = 1.0;
    public double SaturationDelta { get; set; } = 1.0;

    #region IValueConverter Members

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (value is SolidColorBrush brush)
      {
        SolidColorBrush result = brush.ChangeLightness(LightnessDelta).ChangeSaturation(SaturationDelta);
        return result;
      }
      else { return value; }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
      => throw new NotImplementedException();

    #endregion
  }
}
