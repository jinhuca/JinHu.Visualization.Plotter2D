﻿using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace JinHu.Visualization.Plotter2D.Charts
{
  public class PhysicalNavigation : IPlotterElement
  {
    private readonly DispatcherTimer timer;

    public PhysicalNavigation()
    {
      timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(30) };
      timer.Tick += timer_Tick;
    }

    DateTime startTime;
    private void timer_Tick(object sender, EventArgs e)
    {
      TimeSpan time = DateTime.Now - startTime;
      animation.UseMouse = isMouseDown;
      if (!isMouseDown)
      {
        animation.LiquidFrictionQuadraticCoeff = 1;
      }

      plotter.Viewport.Visible = animation.GetValue(time);
      if (animation.IsFinished && !isMouseDown)
      {
        timer.Stop();
      }
    }

    private bool isMouseDown = false;
    private PhysicalRectAnimation animation;
    private Point clickPos;
    private void OnMouseDown(object sender, MouseButtonEventArgs e)
    {
      if (e.ChangedButton == MouseButton.Left)
      {
        clickPos = e.GetPosition(plotter.CentralGrid);

        isMouseDown = true;
        startTime = DateTime.Now;

        animation = new PhysicalRectAnimation(
          plotter.Viewport,
          e.GetPosition(plotter.ViewportPanel));

        timer.Start();
      }
    }

    private void OnMouseUp(object sender, MouseButtonEventArgs e)
    {
      if (e.ChangedButton == MouseButton.Left)
      {
        isMouseDown = false;
        if (clickPos == e.GetPosition(plotter.CentralGrid))
        {
          timer.Stop();
          animation = null;
        }
        else
        {
          if (animation.IsFinished)
          {
            timer.Stop();
            animation = null;
          }
          else
          {
            animation.UseMouse = false;
            animation.LiquidFrictionQuadraticCoeff = 1;
          }
        }
      }
    }

    #region IPlotterElement Members

    private PlotterBase plotter;
    void IPlotterElement.OnPlotterAttached(PlotterBase plotter)
    {
      this.plotter = (PlotterBase)plotter;

      Mouse.AddPreviewMouseDownHandler(plotter.CentralGrid, OnMouseDown);
      Mouse.AddPreviewMouseUpHandler(plotter.CentralGrid, OnMouseUp);
      plotter.CentralGrid.MouseLeave += CentralGrid_MouseLeave;
    }

    private void CentralGrid_MouseLeave(object sender, MouseEventArgs e)
    {
      isMouseDown = false;
    }

    void IPlotterElement.OnPlotterDetaching(PlotterBase plotter)
    {
      plotter.CentralGrid.MouseLeave -= CentralGrid_MouseLeave;
      Mouse.RemovePreviewMouseDownHandler(plotter.CentralGrid, OnMouseDown);
      Mouse.RemovePreviewMouseUpHandler(plotter.CentralGrid, OnMouseUp);

      this.plotter = null;
    }

    PlotterBase IPlotterElement.Plotter
    {
      get { return plotter; }
    }

    #endregion
  }
}
