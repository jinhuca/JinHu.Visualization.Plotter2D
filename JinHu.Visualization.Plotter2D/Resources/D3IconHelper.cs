using System.Reflection;
using System.Windows.Media.Imaging;

namespace JinHu.Visualization.Plotter2D
{
  public static class D3IconHelper
  {
    private static BitmapFrame icon = null;
    public static BitmapFrame TheIcon
    {
      get
      {
        if (icon == null)
        {
          Assembly currentAssembly = typeof(D3IconHelper).Assembly;
          icon = BitmapFrame.Create(currentAssembly.GetManifestResourceStream("JinHu.Visualization.Plotter2D.Resources.D3-icon.ico"));
        }
        return icon;
      }
    }

    private static BitmapFrame whiteIcon = null;
    public static BitmapFrame WhiteIcon
    {
      get
      {
        if (whiteIcon == null)
        {
          Assembly currentAssembly = typeof(D3IconHelper).Assembly;
          whiteIcon = BitmapFrame.Create(currentAssembly.GetManifestResourceStream("JinHu.Visualization.Plotter2D.Resources.D3-icon-white.ico"));
        }

        return whiteIcon;
      }
    }
  }
}
