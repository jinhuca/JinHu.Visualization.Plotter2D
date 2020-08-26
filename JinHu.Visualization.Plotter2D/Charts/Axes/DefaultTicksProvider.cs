namespace JinHu.Visualization.Plotter2D.Charts
{
  internal static class DefaultTicksProvider
  {
    internal static readonly int DefaultTicksCount = 10;

    internal static ITicksInfo<T> GetTicks<T>(this ITicksProvider<T> provider, Range<T> range)
    {
      return provider.GetTicks(range, DefaultTicksCount);
    }
  }
}
