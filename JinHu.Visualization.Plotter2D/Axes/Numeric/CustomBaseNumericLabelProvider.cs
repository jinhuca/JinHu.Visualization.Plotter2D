﻿using System;
using System.Globalization;

namespace JinHu.Visualization.Plotter2D.Charts
{
  public class CustomBaseNumericLabelProvider : LabelProvider<double>
  {
    private double customBase = 2;
    /// <summary>
    /// Gets or sets the custom base.
    /// </summary>
    /// <value>The custom base.</value>
    public double CustomBase
    {
      get { return customBase; }
      set
      {
        if (double.IsNaN(value))
        {
          throw new ArgumentException(Strings.Exceptions.CustomBaseTicksProviderBaseIsNaN);
        }

        if (value <= 0)
        {
          throw new ArgumentOutOfRangeException(Strings.Exceptions.CustomBaseTicksProviderBaseIsTooSmall);
        }

        customBase = value;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomBaseNumericLabelProvider"/> class.
    /// </summary>
    public CustomBaseNumericLabelProvider() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomBaseNumericLabelProvider"/> class.
    /// </summary>
    public CustomBaseNumericLabelProvider(double customBase)
      : this()
    {
      CustomBase = customBase;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomBaseNumericLabelProvider"/> class.
    /// </summary>
    /// <param name="customBase">The custom base.</param>
    /// <param name="customBaseString">The custom base string.</param>
    public CustomBaseNumericLabelProvider(double customBase, string customBaseString)
      : this(customBase)
    {
      CustomBaseString = customBaseString;
    }

    private string customBaseString = null;
    /// <summary>
    /// Gets or sets the custom base string.
    /// </summary>
    /// <value>The custom base string.</value>
    public string CustomBaseString
    {
      get { return customBaseString; }
      set
      {
        if (customBaseString != value)
        {
          customBaseString = value;
          RaiseChanged();
        }
      }
    }

    protected override string GetStringCore(LabelTickInfo<double> tickInfo)
    {
      double value = tickInfo.Tick / customBase;

      string customBaseStr = customBaseString ?? customBase.ToString(CultureInfo.InvariantCulture);
      string result;
      if (value == 1)
      {
        result = customBaseStr;
      }
      else if (value == -1)
      {
        result = "-" + customBaseStr;
      }
      else
      {
        result = value.ToString(CultureInfo.InvariantCulture) + customBaseStr;
      }

      return result;
    }
  }
}
