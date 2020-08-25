﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace JinHu.Visualization.Plotter2D.Common
{
	internal static class Verify
	{
		[DebuggerStepThrough]
		public static void IsTrue(this bool condition)
		{
			if (!condition)
			{
				throw new ArgumentException(Strings.Exceptions.AssertionFailedSearch);
			}
		}

		[DebuggerStepThrough]
		public static void IsTrue(this bool condition, string paramName)
		{
			if (!condition)
			{
				throw new ArgumentException(Strings.Exceptions.AssertionFailedSearch, paramName);
			}
		}

		public static void IsTrueWithMessage(this bool condition, string message)
		{
			if (!condition)
      {
        throw new ArgumentException(message);
      }
    }

		[DebuggerStepThrough]
		public static void AssertNotNull(object obj)
		{
      IsTrue(obj != null);
		}

		public static void VerifyNotNull(this object obj, string paramName)
		{
			if (obj == null)
      {
        throw new ArgumentNullException(paramName);
      }
    }

		public static void VerifyNotNull(this object obj)
		{
			VerifyNotNull(obj, "value");
		}

		[DebuggerStepThrough]
		public static void AssertIsNotNaN(this double d)
		{
      IsTrue(!double.IsNaN(d));
		}

		[DebuggerStepThrough]
		public static void AssertIsFinite(this double d)
		{
      IsTrue(!double.IsInfinity(d) && !(double.IsNaN(d)));
		}
	}
}
