using System;
using System.Collections.Generic;

namespace JinHu.Visualization.Plotter2D.Common
{
  internal sealed class GenericSearcher1d<TCollection, TMember> where TMember : IComparable<TMember>
  {
    public GenericSearcher1d(IList<TCollection> _collection, Func<TCollection, TMember> _selector)
    {
      Collection = _collection ?? throw new ArgumentNullException("collection");
      Selector1 = _selector ?? throw new ArgumentNullException("selector");
    }

    public Func<TCollection, TMember> Selector => Selector1;

    public Func<TCollection, TMember> Selector1 { get; }

    public IList<TCollection> Collection { get; }

    public SearchResult1d SearchBetween(TMember x) => SearchBetween(x, SearchResult1d.Empty);

    public SearchResult1d SearchBetween(TMember _x, SearchResult1d _result)
    {
      if (Collection.Count == 0)
      {
        return SearchResult1d.Empty;
      }

      int lastIndex = Collection.Count - 1;

      if (_x.CompareTo(Selector(Collection[0])) < 0)
      {
        return SearchResult1d.Empty;
      }
      else if (Selector(Collection[lastIndex]).CompareTo(_x) < 0)
      {
        return SearchResult1d.Empty;
      }

      int startIndex = !_result.IsEmpty ? Math.Min(_result.Index, lastIndex) : 0;

      // searching ascending
      if (Selector(Collection[startIndex]).CompareTo(_x) < 0)
      {
        for (int i = startIndex + 1; i <= lastIndex; i++)
        {
          if (Selector(Collection[i]).CompareTo(_x) >= 0)
          {
            return new SearchResult1d { Index = i - 1 };
          }
        }
      }
      else // searching descending
      {
        for (int i = startIndex - 1; i >= 0; i--)
        {
          if (Selector(Collection[i]).CompareTo(_x) <= 0)
          {
            return new SearchResult1d { Index = i };
          }
        }
      }

      throw new InvalidOperationException("Should not appear here.");
    }

    public SearchResult1d SearchFirstLess(TMember x)
    {
      if (Collection.Count == 0)
      {
        return SearchResult1d.Empty;
      }

      SearchResult1d result = SearchResult1d.Empty;
      for (int i = 0; i < Collection.Count; i++)
      {
        if (Selector(Collection[i]).CompareTo(x) >= 0)
        {
          result.Index = i;
          break;
        }
      }

      return result;
    }

    public SearchResult1d SearchGreater(TMember x)
    {
      if (Collection.Count == 0)
      {
        return SearchResult1d.Empty;
      }

      SearchResult1d result = SearchResult1d.Empty;
      for (int i = Collection.Count - 1; i >= 0; i--)
      {
        if (Selector(Collection[i]).CompareTo(x) <= 0)
        {
          result.Index = i;
          break;
        }
      }

      return result;
    }
  }
}