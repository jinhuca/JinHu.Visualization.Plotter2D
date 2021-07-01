﻿using System;
using System.Globalization;

namespace JinHu.Visualization.Plotter2D.Charts
{
  public abstract class NumericLabelProviderBase : LabelProviderBase<double>
  {
    bool shouldRound = true;
    private int rounding;
    protected void Init(double[] ticks)
    {
      if (ticks.Length == 0)
      {
        return;
      }

      double start = ticks[0];
      double finish = ticks[ticks.Length - 1];

      if (start == finish)
      {
        shouldRound = false;
        return;
      }

      double delta = finish - start;

      rounding = (int)Math.Round(Math.Log10(delta));

      double newStart = RoundingHelper.Round(start, rounding);
      double newFinish = RoundingHelper.Round(finish, rounding);
      if (newStart == newFinish)
      {
        rounding--;
      }
    }

    protected override string GetStringCore(LabelTickInfo<double> tickInfo)
    {
      string res;
      if (!shouldRound)
      {
        res = tickInfo.Tick.ToString(CultureInfo.InvariantCulture);
      }
      else
      {
        int round = Math.Min(15, Math.Max(-15, rounding - 3)); // was rounding - 2
        res = RoundingHelper.Round(tickInfo.Tick, round).ToString(CultureInfo.InvariantCulture);
      }

      return res;
    }
  }
}
