using JinHu.Visualization.Plotter2D.Charts;
using JinHu.Visualization.Plotter2D.Common;
using System;
using System.Windows;

namespace JinHu.Visualization.Plotter2D.DataSources
{
  /// <summary>
  ///   Defines warped two-dimensional data source.
  /// </summary>
  /// <typeparam name="T">
  ///   Data piece type.
  /// </typeparam>
  public sealed class WarpedDataSource2D<T> : IDataSource2D<T> where T : struct
  {
    /// <summary>
    ///   Initializes a new instance of the <see cref="WarpedDataSource2D&lt;T&gt;"/> class.
    /// </summary>
    /// <param name="data">Data.</param>
    /// <param name="grid">Grid.</param>
    public WarpedDataSource2D(T[,] data, Point[,] grid)
    {
      if (data == null)
      {
        throw new ArgumentNullException(nameof(data));
      }
      if (grid == null)
      {
        throw new ArgumentNullException(nameof(grid));
      }

      Verify.IsTrue(data.GetLength(0) == grid.GetLength(0));
      Verify.IsTrue(data.GetLength(1) == grid.GetLength(1));

      Data = data;
      Grid = grid;
      Width = data.GetLength(0);
      Height = data.GetLength(1);
    }

    /// <summary>
    ///   Gets two-dimensional data array.
    /// </summary>
    /// <value>
    ///   The data.
    /// </value>
    public T[,] Data { get; }

    /// <summary>
    ///   Gets the grid of data source.
    /// </summary>
    /// <value>
    ///   The grid.
    /// </value>
    public Point[,] Grid { get; }

    /// <summary>
    ///   Gets data grid width.
    /// </summary>
    /// <value>
    ///   The width.
    /// </value>
    public int Width { get; }

    /// <summary>
    ///   Gets data grid height.
    /// </summary>
    /// <value>
    ///   The height.
    /// </value>
    public int Height { get; }

    public IDataSource2D<T> GetSubset(int x0, int y0, int countX, int countY, int stepX, int stepY) => throw new NotImplementedException();
    private void RaiseChanged() => Changed?.Invoke(this, EventArgs.Empty);

    /// <summary>
    /// Occurs when data source changes.
    /// </summary>
    public event EventHandler Changed;
    public Range<T>? Range { get; } = null;
    public T? MissingValue { get; } = null;
  }
}