using System.Diagnostics;
using System.Windows.Media;

namespace JinHu.Visualization.Plotter2D.Common
{
  /// <summary>
  /// Represents a palette that calculates minimal and maximal values of interpolation coefficient and every 100000 calls writes these values 
  /// to DEBUG console.
  /// </summary>
  public class MinMaxLoggingPalette : DecoratorPaletteBase
  {
    int counter = 0;

    public double Min { get; set; } = double.MaxValue;
    public double Max { get; set; } = double.MinValue;

    public override Color GetColor(double t)
    {
      if (t < Min)
      {
        Min = t;
      }

      if (t > Max)
      {
        Max = t;
      }

      counter++;

      if (counter % 100000 == 0)
      {
        Debug.WriteLine("Palette: Min = " + Min + ", max = " + Max);
      }

      return base.GetColor(t);
    }
  }
}
