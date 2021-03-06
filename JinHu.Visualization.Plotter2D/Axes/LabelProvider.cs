﻿using System.Windows;
using System.Windows.Controls;

namespace JinHu.Visualization.Plotter2D.Charts
{
  public abstract class LabelProvider<T> : LabelProviderBase<T>
  {
    public override UIElement[] CreateLabels(ITicksInfo<T> ticksInfo)
    {
      var ticks = ticksInfo.Ticks;

      UIElement[] res = new UIElement[ticks.Length];
      LabelTickInfo<T> labelInfo = new LabelTickInfo<T> { Info = ticksInfo.Info };

      for (int i = 0; i < res.Length; i++)
      {
        labelInfo.Tick = ticks[i];
        labelInfo.Index = i;

        string labelText = GetString(labelInfo);

        TextBlock label = (TextBlock)GetResourceFromPool();
        if (label == null)
        {
          label = new TextBlock();
        }

        label.Text = labelText;
        label.ToolTip = ticks[i].ToString();

        res[i] = label;

        ApplyCustomView(labelInfo, label);
      }

      return res;
    }
  }
}
