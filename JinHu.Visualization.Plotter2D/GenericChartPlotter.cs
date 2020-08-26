using JinHu.Visualization.Plotter2D.Charts;
using System;
using System.Windows;

namespace JinHu.Visualization.Plotter2D
{
  public sealed class GenericChartPlotter<THorizontal, TVertical>
  {
    public AxisBase<THorizontal> HorizontalAxis { get; }

    private readonly AxisBase<TVertical> verticalAxis;
    public AxisBase<TVertical> VerticalAxis => verticalAxis;

    private readonly Plotter plotter;
    public Plotter Plotter => plotter;

    public Func<THorizontal, double> HorizontalToDoubleConverter => HorizontalAxis.ConvertToDouble;

    public Func<double, THorizontal> DoubleToHorizontalConverter => HorizontalAxis.ConvertFromDouble;

    public Func<TVertical, double> VerticalToDoubleConverter => verticalAxis.ConvertToDouble;

    public Func<double, TVertical> DoubleToVerticalConverter => verticalAxis.ConvertFromDouble;

    internal GenericChartPlotter(Plotter plotter) : this(plotter, plotter.MainHorizontalAxis as AxisBase<THorizontal>, plotter.MainVerticalAxis as AxisBase<TVertical>) { }

    internal GenericChartPlotter(Plotter plotter, AxisBase<THorizontal> horizontalAxis, AxisBase<TVertical> verticalAxis)
    {
      this.HorizontalAxis = horizontalAxis ?? throw new ArgumentNullException(Strings.Exceptions.PlotterMainHorizontalAxisShouldNotBeNull);
      this.verticalAxis = verticalAxis ?? throw new ArgumentNullException(Strings.Exceptions.PlotterMainVerticalAxisShouldNotBeNull);
      this.plotter = plotter;
    }

    public GenericRect<THorizontal, TVertical> ViewportRect
    {
      get => CreateGenericRect(plotter.Viewport.Visible);
      set => plotter.Viewport.Visible = CreateRect(value);
    }

    public GenericRect<THorizontal, TVertical> DataRect
    {
      get => CreateGenericRect(plotter.Viewport.Visible.ViewportToData(plotter.Viewport.Transform));
      set => plotter.Viewport.Visible = CreateRect(value).DataToViewport(plotter.Viewport.Transform);
    }

    private DataRect CreateRect(GenericRect<THorizontal, TVertical> value)
    {
      double xMin = HorizontalToDoubleConverter(value.XMin);
      double xMax = HorizontalToDoubleConverter(value.XMax);
      double yMin = VerticalToDoubleConverter(value.YMin);
      double yMax = VerticalToDoubleConverter(value.YMax);

      return new DataRect(new Point(xMin, yMin), new Point(xMax, yMax));
    }

    private GenericRect<THorizontal, TVertical> CreateGenericRect(DataRect rect)
    {
      double xMin = rect.XMin;
      double xMax = rect.XMax;
      double yMin = rect.YMin;
      double yMax = rect.YMax;

      var res = new GenericRect<THorizontal, TVertical>(
        DoubleToHorizontalConverter(xMin),
        DoubleToVerticalConverter(yMin),
        DoubleToHorizontalConverter(xMax),
        DoubleToVerticalConverter(yMax));

      return res;
    }
  }
}