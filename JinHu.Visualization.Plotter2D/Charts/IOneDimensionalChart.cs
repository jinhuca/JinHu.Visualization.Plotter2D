using JinHu.Visualization.Plotter2D.DataSources;
using System;

namespace JinHu.Visualization.Plotter2D
{
  public interface IOneDimensionalChart
  {
    IPointDataSource DataSource { get; set; }
    event EventHandler DataChanged;
  }
}
