﻿using System;
using System.Windows;
using System.Windows.Controls;

namespace JinHu.Visualization.Plotter2D.Charts
{
  /// <summary>
  /// Represents a kind of 'spring' that makes width of one plotter's LeftPanel equal to other plotter's LeftPanel.
  /// </summary>
  public class WidthSpring : FrameworkElement, IPlotterElement
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="WidthSpring"/> class.
    /// </summary>
    public WidthSpring()
    {
    }

    #region Properties

    /// <summary>
    /// Gets or sets panel which is a source of width.
    /// </summary>
    /// <value>The source panel.</value>
    public Panel SourcePanel
    {
      get { return (Panel)GetValue(SourcePanelProperty); }
      set { SetValue(SourcePanelProperty, value); }
    }

    public static readonly DependencyProperty SourcePanelProperty = DependencyProperty.Register(
      "SourcePanel",
      typeof(Panel),
      typeof(WidthSpring),
      new FrameworkPropertyMetadata(null, OnSourcePanelReplaced));

    private static void OnSourcePanelReplaced(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      WidthSpring owner = (WidthSpring)d;
      owner.OnSourcePanelReplaced((Panel)e.OldValue, (Panel)e.NewValue);
    }

    private void OnSourcePanelReplaced(Panel prevPanel, Panel currPanel)
    {
      if (prevPanel != null)
      {
        prevPanel.SizeChanged -= OnPanel_SizeChanged;
      }
      if (currPanel != null)
      {
        currPanel.SizeChanged += OnPanel_SizeChanged;
      }
      UpdateWidth();
    }

    void OnPanel_SizeChanged(object sender, SizeChangedEventArgs e)
    {
      UpdateWidth();
    }

    private void UpdateWidth()
    {
      Panel parentPanel = Parent as Panel;
      if (parentPanel != null && SourcePanel != null)
      {
        Width = Math.Max(SourcePanel.ActualWidth - (parentPanel.ActualWidth - ActualWidth), 0);
      }
    }

    #endregion // end of Properties

    #region IPlotterElement Members

    public void OnPlotterAttached(PlotterBase plotter)
    {
      Plotter = plotter;
      plotter.LeftPanel.Children.Insert(0, this);
    }

    public void OnPlotterDetaching(PlotterBase plotter)
    {
      plotter.LeftPanel.Children.Remove(this);
      Plotter = null;
    }

    public PlotterBase Plotter { get; private set; }

    #endregion
  }
}
