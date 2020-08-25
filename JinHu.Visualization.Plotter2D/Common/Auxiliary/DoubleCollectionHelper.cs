using System.Windows.Media;

namespace JinHu.Visualization.Plotter2D.Common
{
  public static class DoubleCollectionHelper
  {
    public static DoubleCollection Create(params double[] collection)
    {
      return new DoubleCollection(collection);
    }
  }
}
