using System;

namespace JinHu.Visualization.Plotter2D.Charts
{
  public interface IValueConversion<T>
  {
    Func<T, double> ConvertToDouble { get; set; }
    Func<double, T> ConvertFromDouble { get; set; }
  }
}
