using System;
using System.Windows;

namespace JinHu.Visualization.Plotter2D
{
  public class StandardDescription : Description
  {
    public StandardDescription() { }
    public StandardDescription(string description)
    {
      DescriptionString = description ?? throw new ArgumentNullException("description");
    }

    protected override void AttachCore(UIElement element)
    {
      if (DescriptionString == null)
      {
        string str = element.GetType().Name;
        DescriptionString = str;
      }
    }

    public string DescriptionString { get; set; }

    public sealed override string Brief => DescriptionString;

    public sealed override string Full => DescriptionString;
  }
}
