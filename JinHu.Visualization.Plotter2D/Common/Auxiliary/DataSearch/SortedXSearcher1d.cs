using System;
using System.Collections.Generic;
using System.Windows;

namespace JinHu.Visualization.Plotter2D.Common
{
  internal class SortedXSearcher1d
  {
    public SortedXSearcher1d(IList<Point> _collection)
    {
      Collection = _collection ?? throw new ArgumentNullException("collection");
    }

    public IList<Point> Collection { get; }

    public SearchResult1d SearchXBetween(double x) => SearchXBetween(x, SearchResult1d.Empty);

    public SearchResult1d SearchXBetween(double _x, SearchResult1d _result)
    {
      if (Collection.Count == 0)
      {
        return SearchResult1d.Empty;
      }

      int lastIndex = Collection.Count - 1;

      if (_x < Collection[0].X)
      {
        return SearchResult1d.Empty;
      }
      else if (Collection[lastIndex].X < _x)
      {
        return SearchResult1d.Empty;
      }

      int startIndex = !_result.IsEmpty ? Math.Min(_result.Index, lastIndex) : 0;

      // searching ascending
      if (Collection[startIndex].X < _x)
      {
        for (int i = startIndex + 1; i <= lastIndex; i++)
        {
          if (Collection[i].X >= _x)
          {
            return new SearchResult1d { Index = i - 1 };
          }
        }
      }
      else // searching descending
      {
        for (int i = startIndex - 1; i >= 0; i--)
        {
          if (Collection[i].X <= _x)
          {
            return new SearchResult1d { Index = i };
          }
        }
      }

      throw new InvalidOperationException("Should not appear here.");
    }
  }
}