using System.Collections.Generic;

namespace JinHu.Visualization.Plotter2D.DataSources
{
  internal sealed class EnumerableYDataSource<T> : EnumerableDataSource<T>
  {
    public EnumerableYDataSource(IEnumerable<T> data) : base(data) { }
  }
}
