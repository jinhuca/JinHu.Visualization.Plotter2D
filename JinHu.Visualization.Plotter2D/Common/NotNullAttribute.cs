using System;
using System.Diagnostics;

namespace JinHu.Visualization.Plotter2D
{
  [Conditional("DEBUG")]
  [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
  internal sealed class NotNullAttribute : Attribute
  {
  }
}
