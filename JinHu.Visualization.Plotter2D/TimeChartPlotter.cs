using JinHu.Visualization.Plotter2D.Charts;
using System;


namespace JinHu.Visualization.Plotter2D
{
  public class TimeChartPlotter : Plotter
  {
    public TimeChartPlotter()
    {
      MainHorizontalAxis = new HorizontalDateTimeAxis();
    }

    public void SetHorizontalAxisMapping(Func<double, DateTime> fromDouble, Func<DateTime, double> toDouble)
    {
      HorizontalDateTimeAxis axis = (HorizontalDateTimeAxis)MainHorizontalAxis;
      axis.ConvertFromDouble = fromDouble ?? throw new ArgumentNullException(nameof(fromDouble));
      axis.ConvertToDouble = toDouble ?? throw new ArgumentNullException(nameof(toDouble));
    }

    public void SetHorizontalAxisMapping(double min, DateTime minDate, double max, DateTime maxDate)
    {
      HorizontalDateTimeAxis axis = (HorizontalDateTimeAxis)MainHorizontalAxis;
      axis.SetConversion(min, minDate, max, maxDate);
    }
  }
}
