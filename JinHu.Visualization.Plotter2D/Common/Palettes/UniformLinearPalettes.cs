using System.Windows.Media;

namespace JinHu.Visualization.Plotter2D.Common
{
  public static class UniformLinearPalettes
  {
    static UniformLinearPalettes()
    {
      BlackAndWhitePalette.IncreaseBrightness = false;
      RedGreenBluePalette.IncreaseBrightness = false;
      BlueOrangePalette.IncreaseBrightness = false;
    }

    public static UniformLinearPalette BlackAndWhitePalette { get; } = new UniformLinearPalette(Colors.Black, Colors.White);
    public static UniformLinearPalette RedGreenBluePalette { get; } = new UniformLinearPalette(Colors.Blue, Color.FromRgb(0, 255, 0), Colors.Red);
    public static UniformLinearPalette BlueOrangePalette { get; } = new UniformLinearPalette(Colors.Blue, Colors.Cyan, Colors.Yellow, Colors.Orange);
  }
}
