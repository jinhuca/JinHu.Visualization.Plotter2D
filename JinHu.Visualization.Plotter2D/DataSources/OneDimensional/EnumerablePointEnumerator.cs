using System.Collections;
using System.Windows;

namespace JinHu.Visualization.Plotter2D.DataSources
{
  /// <summary>
  /// This enumerator enumerates given enumerable object as sequence of points
  /// </summary>
  /// <typeparam name="T">Type parameter of source IEnumerable</typeparam>
  public sealed class EnumerablePointEnumerator<T> : IPointEnumerator
  {
    public EnumerableDataSource<T> DataSource { get; }

    public IEnumerator Enumerator { get; }

    public EnumerablePointEnumerator(EnumerableDataSource<T> _dataSource)
    {
      DataSource = _dataSource;
      Enumerator = _dataSource.Data.GetEnumerator();
    }

    public bool MoveNext() => Enumerator.MoveNext();

    public void GetCurrent(ref Point p) => DataSource.FillPoint((T)Enumerator.Current, ref p);

    public void ApplyMappings(DependencyObject target) => DataSource.ApplyMappings(target, (T)Enumerator.Current);

    public void Dispose()
    {
      //enumerator.Reset();
    }
  }
}