namespace JinHu.Visualization.Plotter2D.DataSources
{
  public interface INonUniformDataSource2D<T> : IDataSource2D<T> where T : struct
  {
    double[] XCoordinates { get; }
    double[] YCoordinates { get; }
  }
}
