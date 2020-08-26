﻿using System;

namespace JinHu.Visualization.Plotter2D.Charts
{
  public class VerticalIntegerAxis : IntegerAxis
  {
    public VerticalIntegerAxis()
    {
      Placement = AxisPlacement.Left;
    }

    protected override void ValidatePlacement(AxisPlacement newPlacement)
    {
      if (newPlacement == AxisPlacement.Bottom || newPlacement == AxisPlacement.Top)
      {
        throw new ArgumentException(Strings.Exceptions.VerticalAxisCannotBeHorizontal);
      }
    }
  }
}
