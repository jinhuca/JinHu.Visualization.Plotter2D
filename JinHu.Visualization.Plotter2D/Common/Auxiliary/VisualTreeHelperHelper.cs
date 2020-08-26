using System.Windows;
using System.Windows.Media;

namespace JinHu.Visualization.Plotter2D.Common
{
  internal static class VisualTreeHelperHelper
  {
    public static DependencyObject GetParent(DependencyObject target, int depth)
    {
      DependencyObject parent = target;
      do
      {
        parent = VisualTreeHelper.GetParent(parent);
        if (parent == null)
        {
          break;
        }
      } while (--depth > 0);

      return parent;
    }
  }
}
