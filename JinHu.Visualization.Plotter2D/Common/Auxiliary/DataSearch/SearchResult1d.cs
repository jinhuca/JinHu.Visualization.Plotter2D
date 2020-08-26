namespace JinHu.Visualization.Plotter2D.Common
{
  internal struct SearchResult1d
  {
    public static SearchResult1d Empty => new SearchResult1d { Index = -1 };

    public int Index { get; internal set; }

    public bool IsEmpty => Index == -1;

    public override string ToString() => IsEmpty ? "Empty" : $"Index = {Index}";
  }
}
