using System;

namespace JinHu.Visualization.Plotter2D
{
  public sealed class DateTimeHorizontalAxisConstraint : ViewportConstraint
  {
    private readonly double minSeconds = new TimeSpan(DateTime.MinValue.Ticks).TotalSeconds;
    private readonly double maxSeconds = new TimeSpan(DateTime.MaxValue.Ticks).TotalSeconds;

    public override DataRect Apply(DataRect previousDataRect, DataRect proposedDataRect, Viewport2D viewport)
    {
      var borderRect = DataRect.Create(minSeconds, proposedDataRect.YMin, maxSeconds, proposedDataRect.YMax);
      if (proposedDataRect.IntersectsWith(borderRect))
      {
        return DataRect.Intersect(proposedDataRect, borderRect);
      }

      return previousDataRect;
    }
  }
}
