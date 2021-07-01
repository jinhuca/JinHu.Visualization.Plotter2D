using System;

namespace JinHu.Visualization.Plotter2D.Charts
{
  internal sealed class TimeSpanToDoubleConversion
  {
    public TimeSpanToDoubleConversion(TimeSpan minSpan, TimeSpan maxSpan)
      : this(0, minSpan, 1, maxSpan)
    { }

    public TimeSpanToDoubleConversion(double min, TimeSpan minSpan, double max, TimeSpan maxSpan)
    {
      this.min = min;
      length = max - min;
      ticksMin = minSpan.Ticks;
      ticksLength = maxSpan.Ticks - ticksMin;
    }

    private readonly double min;
    private readonly double length;
    private readonly long ticksMin;
    private readonly long ticksLength;

    internal TimeSpan FromDouble(double d)
    {
      double ratio = (d - min) / length;
      long ticks = (long)(ticksMin + ticksLength * ratio);

      ticks = MathHelper.Clamp(ticks, TimeSpan.MinValue.Ticks, TimeSpan.MaxValue.Ticks);

      return new TimeSpan(ticks);
    }

    internal double ToDouble(TimeSpan span)
    {
      double ratio = (span.Ticks - ticksMin) / (double)ticksLength;
      return min + ratio * length;
    }
  }

}
