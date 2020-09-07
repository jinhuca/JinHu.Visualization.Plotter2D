﻿using System;

namespace JinHu.Visualization.Plotter2D.Charts
{
  /// <summary>
  /// AxisControl for DateTime axes.
  /// </summary>
  public class DateTimeAxisControl : AxisControl<DateTime>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="DateTimeAxisControl"/> class.
    /// </summary>
    public DateTimeAxisControl()
    {
      LabelProvider = new DateTimeLabelProvider();
      TicksProvider = new DateTimeTicksProvider();
      MajorLabelProvider = new MajorDateTimeLabelProvider();

      ConvertToDouble = dt => dt.Ticks;

      Range = new Range<DateTime>(DateTime.Now, DateTime.Now.AddYears(1));
    }
  }
}
