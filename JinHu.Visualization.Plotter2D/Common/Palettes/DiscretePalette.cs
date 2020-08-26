using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Markup;
using System.Windows.Media;

namespace JinHu.Visualization.Plotter2D.Common
{
  [ContentProperty("Steps")]
  public class DiscretePalette : IPalette
  {
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public ObservableCollection<LinearPaletteColorStep> Steps { get; } = new ObservableCollection<LinearPaletteColorStep>();

    public DiscretePalette() { }
    public DiscretePalette(params LinearPaletteColorStep[] steps) => Steps.AddMany(steps);

    public Color GetColor(double t)
    {
      if (t <= 0)
      {
        return Steps[0].Color;
      }

      if (t >= Steps.Last().Offset)
      {
        return Steps.Last().Color;
      }

      int i = 0;
      double x = 0;
      while (x < t && i < Steps.Count)
      {
        x = Steps[i].Offset;
        i++;
      }

      Color result = Steps[i - 1].Color;
      return result;
    }

    #region IPalette Members

#pragma warning disable CS0067 // The event 'DiscretePalette.Changed' is never used
    public event EventHandler Changed;
#pragma warning restore CS0067 // The event 'DiscretePalette.Changed' is never used

    #endregion
  }
}
