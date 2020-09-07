using System;

namespace JinHu.Visualization.Plotter2D.Charts
{
  /// <summary>
  /// Represents a horizontal axis with values of <see cref="TimeSpan"/> type.
  /// </summary>
  public class HorizontalTimeSpanAxis : TimeSpanAxis
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="HorizontalTimeSpanAxis"/> class, placed on the bottom of <see cref="Plotter"/>.
    /// </summary>
    public HorizontalTimeSpanAxis()
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
