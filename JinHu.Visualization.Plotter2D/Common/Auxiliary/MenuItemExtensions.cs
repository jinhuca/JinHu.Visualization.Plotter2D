using System.Linq;
using System.Windows.Controls;

namespace JinHu.Visualization.Plotter2D.Common
{
  internal static class MenuItemExtensions
  {
    public static MenuItem FindChildByHeader(this MenuItem parent, string header)
    {
      return parent.Items.OfType<MenuItem>().Where(subMenu => subMenu.Header.ToString() == header).FirstOrDefault();
    }
  }
}
