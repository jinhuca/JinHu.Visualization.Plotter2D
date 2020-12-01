using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace JinHu.Visualization.Plotter2D.DataSources
{
  /// <summary>
  /// Data source that is a composer from several other data sources.
  /// </summary>
  public class CompositeDataSource : IPointDataSource
  {
    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="CompositeDataSource"/> class
    /// </summary>
    public CompositeDataSource() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="CompositeDataSource"/> class.
    /// </summary>
    /// <param name="dataSources">
    /// Data sources.
    /// </param>
    public CompositeDataSource(params IPointDataSource[] dataSources)
    {
      if (dataSources == null)
      {
        throw new ArgumentNullException(nameof(dataSources));
      }

      foreach (var dataSource in dataSources)
      {
        AddDataPart(dataSource);
      }
    }

    #endregion Constructors

    #region 

    private readonly List<IPointDataSource> dataParts = new List<IPointDataSource>();

    public IEnumerable<IPointDataSource> DataParts => dataParts;

    /// <summary>
    ///   Adds data part.
    /// </summary>
    /// <param name="dataPart">
    ///   The data part.
    /// </param>
    public void AddDataPart(IPointDataSource dataPart)
    {
      if (dataPart == null)
      {
        throw new ArgumentNullException(nameof(dataPart));
      }

      dataParts.Add(dataPart);
      dataPart.DataChanged += OnPartDataChanged;
    }

    private void OnPartDataChanged(object sender, EventArgs e) => RaiseDataChanged();

    #endregion 

    #region IPointSource Members

    public event EventHandler DataChanged;
    protected void RaiseDataChanged()
    {
      DataChanged?.Invoke(this, EventArgs.Empty);
    }

    public IPointEnumerator GetEnumerator(DependencyObject context) => new CompositeEnumerator(this, context);

    #endregion
 
    private sealed class CompositeEnumerator : IPointEnumerator
    {
      private readonly IEnumerable<IPointEnumerator> enumerators;

      public CompositeEnumerator(CompositeDataSource dataSource, DependencyObject context)
      {
        enumerators = dataSource.dataParts.Select(part => part.GetEnumerator(context)).ToList();
      }

      #region IChartPointEnumerator Members

      public bool MoveNext()
      {
        bool res = false;
        foreach (var enumerator in enumerators)
        {
          res |= enumerator.MoveNext();
        }
        return res;
      }

      public void ApplyMappings(DependencyObject glyph)
      {
        foreach (var enumerator in enumerators)
        {
          enumerator.ApplyMappings(glyph);
        }
      }

      public void GetCurrent(ref Point p)
      {
        foreach (var enumerator in enumerators)
        {
          enumerator.GetCurrent(ref p);
        }
      }

      public void Dispose()
      {
        foreach (var enumerator in enumerators)
        {
          enumerator.Dispose();
        }
      }

      #endregion
    }
  }
}