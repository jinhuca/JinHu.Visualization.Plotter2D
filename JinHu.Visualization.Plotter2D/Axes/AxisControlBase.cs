using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace JinHu.Visualization.Plotter2D.Charts
{
  public abstract class AxisControlBase : ContentControl
  {
    #region Properties

    public HorizontalAlignment LabelsHorizontalAlignment
    {
      get { return (HorizontalAlignment)GetValue(LabelsHorizontalAlignmentProperty); }
      set { SetValue(LabelsHorizontalAlignmentProperty, value); }
    }

    public static readonly DependencyProperty LabelsHorizontalAlignmentProperty = DependencyProperty.Register(
      "LabelsHorizontalAlignment",
      typeof(HorizontalAlignment),
      typeof(AxisControlBase),
      new FrameworkPropertyMetadata(HorizontalAlignment.Center));


    public VerticalAlignment LabelsVerticalAlignment
    {
      get { return (VerticalAlignment)GetValue(LabelsVerticalAlignmentProperty); }
      set { SetValue(LabelsVerticalAlignmentProperty, value); }
    }

    public static readonly DependencyProperty LabelsVerticalAlignmentProperty = DependencyProperty.Register(
      "LabelsVerticalAlignment",
      typeof(VerticalAlignment),
      typeof(AxisControlBase),
      new FrameworkPropertyMetadata(VerticalAlignment.Center));

    public abstract Path TicksPath { get; }

    #endregion // end of Properties
  }
}
