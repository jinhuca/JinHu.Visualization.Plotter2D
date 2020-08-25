﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JinHu.Visualization.Plotter2D.Charts
{
	public class TimeSpanAxisControl : AxisControl<TimeSpan>
	{
		public TimeSpanAxisControl()
		{
			LabelProvider = new TimeSpanLabelProvider();
			TicksProvider = new TimeSpanTicksProvider();

			ConvertToDouble = time => time.Ticks;

			Range = new Range<TimeSpan>(new TimeSpan(), new TimeSpan(1, 0, 0));
		}
	}
}
