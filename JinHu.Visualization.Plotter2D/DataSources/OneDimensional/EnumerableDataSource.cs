using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;

namespace JinHu.Visualization.Plotter2D.DataSources
{
  public class EnumerableDataSource<T> : EnumerableDataSourceBase<T>
  {
    public EnumerableDataSource(IEnumerable<T> data) : base(data) { }
    public EnumerableDataSource(IEnumerable data) : base(data) { }

    public Func<T, Point> XYMapping
    {
      get => GetXyMapping();
      set => SetXYMapping(value);
    }

    internal List<Mapping<T>> Mappings { get; } = new List<Mapping<T>>();
    public Func<T, double> XMapping { get; set; }
    public Func<T, double> YMapping { get; set; }

    private Func<T, Point> xyMapping;

    public Func<T, Point> GetXyMapping() => xyMapping;

    public void SetXyMapping(Func<T, Point> value) => xyMapping = value;

    public void SetXMapping(Func<T, double> mapping)
    {
      XMapping = mapping ?? throw new ArgumentNullException(nameof(mapping));
      RaiseDataChanged();
    }

    public void SetYMapping(Func<T, double> mapping)
    {
      YMapping = mapping ?? throw new ArgumentNullException(nameof(mapping));
      RaiseDataChanged();
    }

#pragma warning disable CS3005 // Identifier 'EnumerableDataSource<T>.SetXYMapping(Func<T, Point>)' differing only in case is not CLS-compliant
    public void SetXYMapping(Func<T, Point> mapping)
#pragma warning restore CS3005 // Identifier 'EnumerableDataSource<T>.SetXYMapping(Func<T, Point>)' differing only in case is not CLS-compliant
    {
      SetXyMapping(mapping ?? throw new ArgumentNullException(nameof(mapping)));
      RaiseDataChanged();
    }

    public void AddMapping(DependencyProperty property, Func<T, object> mapping)
    {
      if (property == null)
      {
        throw new ArgumentNullException(nameof(property));
      }
      if (mapping == null)
      {
        throw new ArgumentNullException(nameof(mapping));
      }
      Mappings.Add(new Mapping<T> { Property = property, F = mapping });
    }

    public override IPointEnumerator GetEnumerator(DependencyObject context) => new EnumerablePointEnumerator<T>(this);

    internal void FillPoint(T elem, ref Point point)
    {
      if (GetXyMapping() != null)
      {
        point = GetXyMapping()(elem);
      }
      else
      {
        if (XMapping != null)
        {
          point.X = XMapping(elem);
        }
        if (YMapping != null)
        {
          point.Y = YMapping(elem);
        }
      }
    }

    internal void ApplyMappings(DependencyObject target, T elem)
    {
      if (target != null)
      {
        foreach (var mapping in Mappings)
        {
          target.SetValue(mapping.Property, mapping.F(elem));
        }
      }
    }
  }
}