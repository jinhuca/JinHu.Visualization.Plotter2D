using System.Windows.Data;

namespace JinHu.Visualization.Plotter2D
{
  public class TemplateBinding : Binding
  {
    public TemplateBinding()
    {
      RelativeSource = new RelativeSource { Mode = RelativeSourceMode.TemplatedParent };
    }

    public TemplateBinding(string propertyPath) : this()
    {
      Path = new System.Windows.PropertyPath(propertyPath);
    }
  }
}
