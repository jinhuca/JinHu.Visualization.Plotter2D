using System;

namespace JinHu.Visualization.Plotter2D.Charts
{
  public class HorizontalIntegerAxis : IntegerAxis
  {
    public HorizontalIntegerAxis()
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
