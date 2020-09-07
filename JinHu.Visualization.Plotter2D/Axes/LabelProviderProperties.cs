using System.Windows;

namespace JinHu.Visualization.Plotter2D.Charts
{
  internal class LabelProviderProperties : DependencyObject
  {
    public static bool GetExponentialIsCommonLabel(DependencyObject obj)
    {
      return (bool)obj.GetValue(ExponentialIsCommonLabelProperty);
    }

    public static void SetExponentialIsCommonLabel(DependencyObject obj, bool value)
    {
      obj.SetValue(ExponentialIsCommonLabelProperty, value);
    }

    public static readonly DependencyProperty ExponentialIsCommonLabelProperty = DependencyProperty.RegisterAttached(
      "ExponentialIsCommonLabel",
      typeof(bool),
      typeof(LabelProviderProperties),
      new FrameworkPropertyMetadata(true));
  }
}
