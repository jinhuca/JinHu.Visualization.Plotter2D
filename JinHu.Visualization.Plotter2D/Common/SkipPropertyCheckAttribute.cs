using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace JinHu.Visualization.Plotter2D.Common
{
	[Conditional("DEBUG")]
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	internal sealed class SkipPropertyCheckAttribute : Attribute
	{
	}
}
