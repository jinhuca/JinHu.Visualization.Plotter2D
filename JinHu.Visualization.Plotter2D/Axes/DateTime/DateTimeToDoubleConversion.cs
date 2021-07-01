using System;

namespace JinHu.Visualization.Plotter2D.Charts
{
  internal sealed class DateTimeToDoubleConversion
  {
    public DateTimeToDoubleConversion(double min, DateTime minDate, double max, DateTime maxDate)
    {
      this.min = min;
      length = max - min;
      ticksMin = minDate.Ticks;
      ticksLength = maxDate.Ticks - ticksMin;
    }

    private readonly double min;
    private readonly double length;
    private readonly long ticksMin;
    private readonly long ticksLength;

    internal DateTime FromDouble(double d)
    {
      double ratio = (d - min) / length;
      long tick = (long)(ticksMin + ticksLength * ratio);

      tick = MathHelper.Clamp(tick, DateTime.MinValue.Ticks, DateTime.MaxValue.Ticks);

      return new DateTime(tick);
    }

    internal double ToDouble(DateTime dt)
    {
      double ratio = (dt.Ticks - ticksMin) / (double)ticksLength;
      return min + ratio * length;
    }
  }
}
