using System;
using System.Windows.Markup;

namespace JinHu.Visualization.Plotter2D
{
  /// <summary>
  ///   Represents a markup extension, which allows to get an access to application resource files.
  /// </summary>
  [MarkupExtensionReturnType(typeof(string))]
  public class ResourceExtension : MarkupExtension
  {
    /// <summary>
    ///   Initializes a new instance of the <see cref="ResourceExtension"/> class.
    /// </summary>
    public ResourceExtension() { }

    private string resourceKey;
    //[ConstructorArgument("resourceKey")]
    public string ResourceKey
    {
      get { return resourceKey; }
      set
      {
        if (resourceKey == null)
        {
          throw new ArgumentNullException("resourceKey");
        }

        resourceKey = value;
      }
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="ResourceExtension"/> class.
    /// </summary>
    /// <param name="_resourceKey">
    ///   The resource key.
    /// </param>
    public ResourceExtension(string _resourceKey)
    {
      if (_resourceKey == null)
      {
        throw new ArgumentNullException("resourceKey");
      }

      resourceKey = _resourceKey;
    }

    public override object ProvideValue(IServiceProvider serviceProvider) => Strings.UIResources.ResourceManager.GetString(resourceKey);
  }
}
