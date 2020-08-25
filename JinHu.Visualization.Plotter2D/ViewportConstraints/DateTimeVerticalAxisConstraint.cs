using System;

namespace JinHu.Visualization.Plotter2D
{
  public sealed class DateTimeVerticalAxisConstraint : ViewportConstraint
  {
    private readonly double minSeconds = new TimeSpan(DateTime.MinValue.Ticks).TotalSeconds;
    private readonly double maxSeconds = new TimeSpan(DateTime.MaxValue.Ticks).TotalSeconds;

    public override DataRect Apply(DataRect previousDataRect, DataRect proposedDataRect, Viewport2D viewport)
    {
      DataRect borderRect = DataRect.Create(proposedDataRect.XMin, minSeconds, proposedDataRect.XMax, maxSeconds);
      if (proposedDataRect.IntersectsWith(borderRect))
      {
        return DataRect.Intersect(proposedDataRect, borderRect);
      }

      return previousDataRect;
    }
  }
}
