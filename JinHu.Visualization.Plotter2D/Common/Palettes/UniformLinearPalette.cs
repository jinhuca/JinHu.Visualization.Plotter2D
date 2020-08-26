using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows.Markup;
using System.Windows.Media;

namespace JinHu.Visualization.Plotter2D.Common
{
  [ContentProperty("ColorSteps")]
  public sealed class UniformLinearPalette : IPalette, ISupportInitialize
  {
    public ObservableCollection<Color> Colors { get; private set; } = new ObservableCollection<Color>();

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public List<PaletteColorStep> ColorSteps { get; } = new List<PaletteColorStep>();

    [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
    private void RaiseChanged() => Changed.Raise(this);
    public event EventHandler Changed;

    public UniformLinearPalette() { }

    public UniformLinearPalette(params Color[] colors)
    {
      if (colors == null)
      {
        throw new ArgumentNullException("colors");
      }

      if (colors.Length < 2)
      {
        throw new ArgumentException(Strings.Exceptions.PaletteTooFewColors);
      }

      Colors = new ObservableCollection<Color>(colors);
      FillPoints(colors.Length);
    }

    private void FillPoints(int size)
    {
      Points = new double[size];
      for (int i = 0; i < size; i++)
      {
        Points[i] = i / (double)(size - 1);
      }
    }
    [DefaultValue(true)]
    public bool IncreaseBrightness { get; set; } = true;

    public double[] Points { get; set; }
    public bool BeganInit { get; set; } = false;

    public Color GetColor(double t)
    {
      Verify.AssertIsFinite(t);
      Verify.IsTrue(0 <= t && t <= 1);

      if (t <= 0)
      {
        return Colors[0];
      }
      else if (t >= 1)
      {
        return Colors[Colors.Count - 1];
      }
      else
      {
        int i = 0;
        while (Points[i] < t)
        {
          i++;
        }

        double ratio = (Points[i] - t) / (Points[i] - Points[i - 1]);

        Verify.IsTrue(0 <= ratio && ratio <= 1);

        Color c0 = Colors[i - 1];
        Color c1 = Colors[i];
        Color res = Color.FromRgb(
          (byte)(c0.R * ratio + c1.R * (1 - ratio)),
          (byte)(c0.G * ratio + c1.G * (1 - ratio)),
          (byte)(c0.B * ratio + c1.B * (1 - ratio)));

        // Increasing saturation and brightness
        if (IncreaseBrightness)
        {
          HsbColor hsb = res.ToHsbColor();
          //hsb.Saturation = 0.5 * (1 + hsb.Saturation);
          hsb.Brightness = 0.5 * (1 + hsb.Brightness);
          return hsb.ToArgbColor();
        }
        else
        {
          return res;
        }
      }
    }

    #region ISupportInitialize Members

    public void BeginInit() => BeganInit = true;

    public void EndInit()
    {
      if (BeganInit)
      {
        Colors = new ObservableCollection<Color>(ColorSteps.Select(step => step.Color));
        FillPoints(Colors.Count);
      }
    }

    #endregion
  }
}
