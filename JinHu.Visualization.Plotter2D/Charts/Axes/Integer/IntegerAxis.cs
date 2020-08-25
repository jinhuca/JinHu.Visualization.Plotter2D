using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JinHu.Visualization.Plotter2D.Charts
{
	public class IntegerAxis : AxisBase<int>
	{
		public IntegerAxis()
			: base(new IntegerAxisControl(),
				d => (int)d,
				i => (double)i)
		{

		}
	}
}
