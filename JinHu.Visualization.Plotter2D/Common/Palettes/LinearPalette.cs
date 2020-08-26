using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Markup;
using System.Windows.Media;

namespace JinHu.Visualization.Plotter2D.Common
{
  /// <summary>
  /// Represents a palette with start and stop colors and intermediate colors with their custom offsets.
  /// </summary>
  [ContentProperty("Steps")]
  public class LinearPalette : PaletteBase, ISupportInitialize
  {
    public ObservableCollection<LinearPaletteColorStep> Steps { get; } = new ObservableCollection<LinearPaletteColorStep>();

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public Color StartColor { get; set; } = Colors.White;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public Color EndColor { get; set; } = Colors.Black;

    /// <summary>
    /// Initializes a new instance of the <see cref="LinearPalette"/> class.
    /// </summary>
    public LinearPalette() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="LinearPalette"/> class.
    /// </summary>
    /// <param name="startColor">The start color.</param>
    /// <param name="endColor">The end color.</param>
    /// <param name="steps">The steps.</param>
    public LinearPalette(Color startColor, Color endColor, params LinearPaletteColorStep[] steps)
    {
      Steps.Add(new LinearPaletteColorStep(startColor, 0));
      if (steps != null)
      {
        Steps.AddMany(steps);
      }

      Steps.Add(new LinearPaletteColorStep(endColor, 1));
    }

    #region IPalette Members

    /// <summary>
    /// Gets the color by interpolation coefficient.
    /// </summary>
    /// <param name="t">Interpolation coefficient, should belong to [0..1].</param>
    /// <returns>Color.</returns>
    public override Color GetColor(double t)
    {
      if (t < 0)
      {
        return Steps[0].Color;
      }

      if (t > 1)
      {
        return Steps[Steps.Count - 1].Color;
      }

      int i = 0;
      double x = 0;
      while (x <= t)
      {
        x = Steps[i + 1].Offset;
        i++;
      }

      double ratio = (t - Steps[i - 1].Offset) / (Steps[i].Offset - Steps[i - 1].Offset);

      Color c0 = Steps[i - 1].Color;
      Color c1 = Steps[i].Color;

      return Color.FromRgb(
        (byte)((1 - ratio) * c0.R + ratio * c1.R),
        (byte)((1 - ratio) * c0.G + ratio * c1.G),
        (byte)((1 - ratio) * c0.B + ratio * c1.B));
    }

    #endregion

    #region ISupportInitialize Members

    void ISupportInitialize.BeginInit()
    {
    }

    void ISupportInitialize.EndInit()
    {
      if (Steps.Count == 0 || Steps[0].Offset > 0)
      {
        Steps.Insert(0, new LinearPaletteColorStep(StartColor, 0));
      }

      if (Steps.Count == 0 || Steps[Steps.Count - 1].Offset < 1)
      {
        Steps.Add(new LinearPaletteColorStep(EndColor, 1));
      }
    }

    #endregion
  }
}
