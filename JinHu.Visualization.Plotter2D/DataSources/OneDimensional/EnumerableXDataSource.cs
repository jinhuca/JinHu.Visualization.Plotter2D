using System.Collections.Generic;

namespace JinHu.Visualization.Plotter2D.DataSources
{
  internal sealed class EnumerableXDataSource<T> : EnumerableDataSource<T>
  {
    public EnumerableXDataSource(IEnumerable<T> data) : base(data) { }
  }
}
