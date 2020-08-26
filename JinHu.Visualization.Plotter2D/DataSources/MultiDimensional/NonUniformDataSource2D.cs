using System;
using System.Windows;

namespace JinHu.Visualization.Plotter2D.DataSources
{
  public class NonUniformDataSource2D<T> : INonUniformDataSource2D<T> where T : struct
  {
    public NonUniformDataSource2D(double[] xcoordinates, double[] ycoordinates, T[,] data)
    {
      XCoordinates = xcoordinates ?? throw new ArgumentNullException(nameof(xcoordinates));
      YCoordinates = ycoordinates ?? throw new ArgumentNullException(nameof(ycoordinates));
      BuildGrid();
      Data = data ?? throw new ArgumentNullException(nameof(data));
    }

    private void BuildGrid()
    {
      Grid = new Point[Width, Height];
      for (int iy = 0; iy < Height; iy++)
      {
        for (int ix = 0; ix < Width; ix++)
        {
          Grid[ix, iy] = new Point(XCoordinates[ix], YCoordinates[iy]);
        }
      }
    }

    public double[] XCoordinates { get; }
    public double[] YCoordinates { get; }
    public T[,] Data { get; }
    public IDataSource2D<T> GetSubset(int x0, int y0, int countX, int countY, int stepX, int stepY) => throw new NotImplementedException();
    public void ApplyMappings(DependencyObject marker, int x, int y) => throw new NotImplementedException();
    public Point[,] Grid { get; private set; }
    public int Width => XCoordinates.Length;
    public int Height => YCoordinates.Length;
#pragma warning disable CS0067 // The event 'NonUniformDataSource2D<T>.Changed' is never used
    public event EventHandler Changed;
#pragma warning restore CS0067 // The event 'NonUniformDataSource2D<T>.Changed' is never used

    #region IDataSource2D<T> Members

    public Charts.Range<T>? Range => throw new NotImplementedException();
    public T? MissingValue => throw new NotImplementedException();

    #endregion
  }
}
