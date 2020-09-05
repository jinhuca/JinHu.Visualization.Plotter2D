using System.Collections.Generic;
using System.Windows;

namespace JinHu.Visualization.Plotter2D.DataSources
{
  public static class DataSourceExtensions
  {
    public static RawDataSource AsDataSource(this IEnumerable<Point> points)
      => new RawDataSource(points);

    public static EnumerableDataSource<T> AsDataSource<T>(this IEnumerable<T> collection)
      => new EnumerableDataSource<T>(collection);

    public static EnumerableDataSource<T> AsXDataSource<T>(this IEnumerable<T> collection)
    {
      if (typeof(T) == typeof(double))
      {
        return ((IEnumerable<double>)collection).AsXDataSource() as EnumerableDataSource<T>;
      }
      else if (typeof(T) == typeof(float))
      {
        return ((IEnumerable<float>)collection).AsXDataSource() as EnumerableDataSource<T>;
      }
      return new EnumerableXDataSource<T>(collection);
    }

    public static EnumerableDataSource<float> AsXDataSource(this IEnumerable<float> collection)
    {
      EnumerableXDataSource<float> ds = new EnumerableXDataSource<float>(collection);
      ds.XMapping = f => f;
      return ds;
    }

    public static EnumerableDataSource<T> AsYDataSource<T>(this IEnumerable<T> collection)
    {
      if (typeof(T) == typeof(double))
      {
        return ((IEnumerable<double>)collection).AsYDataSource() as EnumerableDataSource<T>;
      }
      else if (typeof(T) == typeof(float))
      {
        return ((IEnumerable<float>)collection).AsYDataSource() as EnumerableDataSource<T>;
      }
      return new EnumerableYDataSource<T>(collection);
    }

    public static EnumerableDataSource<double> AsXDataSource(this IEnumerable<double> collection)
    {
      EnumerableXDataSource<double> ds = new EnumerableXDataSource<double>(collection);
      ds.XMapping = x => x;
      //ds.SetXMapping(x => x);
      return ds;
    }

    public static EnumerableDataSource<double> AsYDataSource(this IEnumerable<double> collection)
    {
      EnumerableYDataSource<double> ds = new EnumerableYDataSource<double>(collection);
      ds.YMapping = y => y;
      //ds.SetYMapping(y => y);
      return ds;
    }

    public static EnumerableDataSource<float> AsYDataSource(this IEnumerable<float> collection)
    {
      EnumerableYDataSource<float> ds = new EnumerableYDataSource<float>(collection);
      ds.YMapping = f => f;
      //ds.SetYMapping(f => f);
      return ds;
    }

    public static CompositeDataSource Join(this IPointDataSource ds1, IPointDataSource ds2)
      => new CompositeDataSource(ds1, ds2);

    public static IEnumerable<Point> GetPoints(this IPointDataSource dataSource)
      => DataSourceHelper.GetPoints(dataSource);

    public static IEnumerable<Point> GetPoints(this IPointDataSource dataSource, DependencyObject context)
      => DataSourceHelper.GetPoints(dataSource, context);
  }
}