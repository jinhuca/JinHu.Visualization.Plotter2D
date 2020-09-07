using System;

namespace JinHu.Visualization.Plotter2D.Charts
{
  /// <summary>
  /// Represents an axis with ticks of <see cref="System.DateTime"/> type, which can be placed only from bottom or top of <see cref="PlotterBase"/>.
  /// By default is placed from bottom.
  /// </summary>
  public class HorizontalDateTimeAxis : DateTimeAxis
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="HorizontalDateTimeAxis"/> class.
    /// </summary>
    public HorizontalDateTimeAxis()
    {
      Placement = AxisPlacement.Bottom;
    }

    protected override void ValidatePlacement(AxisPlacement newPlacement)
    {
      if (newPlacement == AxisPlacement.Left || newPlacement == AxisPlacement.Right)
      {
        throw new ArgumentException(Strings.Exceptions.HorizontalAxisCannotBeVertical);
      }
    }
  }
}
