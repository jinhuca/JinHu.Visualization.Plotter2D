namespace JinHu.Visualization.Plotter2D
{
  public static class DataDomains
  {
    public static DataRect XPositive { get; } = DataRect.FromPoints(double.Epsilon, double.MinValue / 2, double.MaxValue, double.MaxValue / 2);
    public static DataRect YPositive { get; } = DataRect.FromPoints(double.MinValue / 2, double.Epsilon, double.MaxValue / 2, double.MaxValue);
    public static DataRect XYPositive { get; } = DataRect.FromPoints(double.Epsilon, double.Epsilon, double.MaxValue, double.MaxValue);
  }
}
