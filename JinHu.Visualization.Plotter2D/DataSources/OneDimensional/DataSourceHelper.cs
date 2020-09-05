using JinHu.Visualization.Plotter2D.Charts;
using System;
using System.Collections.Generic;
using System.Windows;

namespace JinHu.Visualization.Plotter2D.DataSources
{
  public static class DataSourceHelper
  {
    public static IEnumerable<Point> GetPoints(IPointDataSource dataSource) => GetPoints(dataSource, null);

    public static IEnumerable<Point> GetPoints(IPointDataSource dataSource, DependencyObject context)
    {
      if (dataSource == null)
      {
        throw new ArgumentNullException(nameof(dataSource));
      }

      if (context == null)
      {
        context = new DataSource2dContext();
      }

      using IPointEnumerator enumerator = dataSource.GetEnumerator(context);
      Point p = new Point();
      while (enumerator.MoveNext())
      {
        enumerator.GetCurrent(ref p);
        yield return p;
        p = new Point();
      }
    }
  }
}