using JinHu.Visualization.Plotter2D.Charts;
using JinHu.Visualization.Plotter2D.Common;
using System;
using System.Windows;
using System.Windows.Controls;

namespace JinHu.Visualization.Plotter2D
{
  /// <summary>
  ///   Represents a title of horizontal axis. Can be placed from top or bottom of Plotter.
  /// </summary>
  public class HorizontalAxisTitle : ContentControl, IPlotterElement
  {
    /// <summary>
    ///   Initializes a new instance of the <see cref="HorizontalAxisTitle"/> class.
    /// </summary>
    public HorizontalAxisTitle()
    {
      FontSize = 16;
      HorizontalAlignment = HorizontalAlignment.Center;
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="HorizontalAxisTitle"/> class.
    /// </summary>
    /// <param name="content">The content.</param>
    public HorizontalAxisTitle(object content) : this() => Content = content;

    private PlotterBase plotter;
    public PlotterBase Plotter => plotter;

    public void OnPlotterAttached(PlotterBase _plotter)
    {
      plotter = _plotter;
      AddToPlotter();
    }

    public void OnPlotterDetaching(PlotterBase _plotter)
    {
      RemoveFromPlotter();
      plotter = null;
    }

    private Panel GetHostPanel(PlotterBase _plotter) => placement == AxisPlacement.Bottom ? _plotter.BottomPanel : _plotter.TopPanel;

    private int GetInsertPosition(Panel panel) => placement == AxisPlacement.Bottom ? panel.Children.Count : 0;

    private AxisPlacement placement = AxisPlacement.Bottom;

    /// <summary>
    ///   Gets or sets the placement of axis title.
    /// </summary>
    /// <value>The placement.</value>
    public AxisPlacement Placement
    {
      get { return placement; }
      set
      {
        if (!value.IsBottomOrTop())
        {
          throw new ArgumentException(string.Format("HorizontalAxisTitle only supports Top and Bottom values of AxisPlacement, you passed '{0}'", value), "Placement");
        }
        if (placement != value)
        {
          if (plotter != null)
          {
            RemoveFromPlotter();
          }
          placement = value;
          if (plotter != null)
          {
            AddToPlotter();
          }
        }
      }
    }

    private void RemoveFromPlotter()
    {
      var oldPanel = GetHostPanel(plotter);
      oldPanel.Children.Remove(this);
    }

    private void AddToPlotter()
    {
      var hostPanel = GetHostPanel(plotter);
      var index = GetInsertPosition(hostPanel);
      hostPanel.Children.Insert(index, this);
    }
  }
}