using JinHu.Visualization.Plotter2D.Charts;
using JinHu.Visualization.Plotter2D.Common;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace JinHu.Visualization.Plotter2D
{
  /// <summary>
  /// Represents a title of vertical axis. Can be placed from left or right of Plotter.
  /// </summary>
  public class VerticalAxisTitle : ContentControl, IPlotterElement
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="VerticalAxisTitle"/> class.
    /// </summary>
    public VerticalAxisTitle()
    {
      ChangeLayoutTransform();
      VerticalAlignment = VerticalAlignment.Center;
      FontSize = 16;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="VerticalAxisTitle"/> class.
    /// </summary>
    /// <param name="content">The content.</param>
    public VerticalAxisTitle(object content) : this() => Content = content;

    private void ChangeLayoutTransform()
    {
      LayoutTransform = placement == AxisPlacement.Left ? new RotateTransform(-90) : new RotateTransform(90);
    }
    public PlotterBase Plotter { get; private set; }

    public void OnPlotterAttached(PlotterBase plotter)
    {
      Plotter = plotter;
      var hostPanel = GetHostPanel(plotter);
      var index = GetInsertPosition(hostPanel);
      hostPanel.Children.Insert(index, this);
    }

    public void OnPlotterDetaching(PlotterBase plotter)
    {
      Plotter = null;
      var hostPanel = GetHostPanel(plotter);
      hostPanel.Children.Remove(this);
    }

    private Panel GetHostPanel(PlotterBase plotter) => placement == AxisPlacement.Left ? plotter.LeftPanel : plotter.RightPanel;

    private int GetInsertPosition(Panel panel) => placement == AxisPlacement.Left ? 0 : panel.Children.Count;

    private AxisPlacement placement = AxisPlacement.Left;
    /// <summary>
    /// Gets or sets the placement of axis title.
    /// </summary>
    /// <value>The placement.</value>
    public AxisPlacement Placement
    {
      get { return placement; }
      set
      {
        if (value.IsBottomOrTop())
        {
          throw new ArgumentException(string.Format("VerticalAxisTitle only supports Left and Right values of AxisPlacement, you passed '{0}'", value), "Placement");
        }

        if (placement != value)
        {
          if (Plotter != null)
          {
            var oldPanel = GetHostPanel(Plotter);
            oldPanel.Children.Remove(this);
          }

          placement = value;

          ChangeLayoutTransform();

          if (Plotter != null)
          {
            var hostPanel = GetHostPanel(Plotter);
            var index = GetInsertPosition(hostPanel);
            hostPanel.Children.Insert(index, this);
          }
        }
      }
    }
  }
}