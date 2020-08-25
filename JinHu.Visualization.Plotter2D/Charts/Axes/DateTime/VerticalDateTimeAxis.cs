﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JinHu.Visualization.Plotter2D;

namespace JinHu.Visualization.Plotter2D.Charts
{
	public class VerticalDateTimeAxis : DateTimeAxis
	{
		public VerticalDateTimeAxis()
		{
			Placement = AxisPlacement.Left;
			Constraint = new DateTimeVerticalAxisConstraint();
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
