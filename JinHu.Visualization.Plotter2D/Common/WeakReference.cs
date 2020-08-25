using System;

namespace JinHu.Visualization.Plotter2D.Common
{
  internal sealed class WeakReference<T>
  {
    public WeakReference(WeakReference reference) => Reference = reference;
    public WeakReference(T referencedObject) => Reference = new WeakReference(referencedObject);
    public bool IsAlive => Reference.IsAlive;
    public T Target => (T)Reference.Target;
    public WeakReference Reference { get; }
  }
}
