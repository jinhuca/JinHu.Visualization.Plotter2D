using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Markup;

namespace JinHu.Visualization.Plotter2D
{
  [EditorBrowsable(EditorBrowsableState.Never)]
  public class XbapConditionalExpression : MarkupExtension
  {
    public XbapConditionalExpression() { }

    public XbapConditionalExpression(object value)
    {
      Value = value;
    }

    [ConstructorArgument("value")]
    public object Value { get; set; }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
#if RELEASEXBAP
			return null;
#else
      return ((ResourceDictionary)Application.LoadComponent(new Uri(Constants.ThemeUri, UriKind.Relative)))[Value];
#endif
    }
  }
}
