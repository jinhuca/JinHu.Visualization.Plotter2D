using JinHu.Visualization.Plotter2D.Common;
using System;
using System.ComponentModel;
using System.Diagnostics;
using static System.Math;

namespace JinHu.Visualization.Plotter2D.Charts
{
  /// <summary>
  /// An ordered pair of values, representing a segment.
  /// </summary>
  /// <typeparam name="T">Type of each of two values of range.</typeparam>
  [Serializable]
  [DebuggerDisplay(@"{Min} — {Max}")]
  [TypeConverter(typeof(RangeConverter))]
  public struct Range<T> : IEquatable<Range<T>>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Range&lt;T&gt;"/> struct.
    /// </summary>
    /// <param name="min">The minimal value of segment.</param>
    /// <param name="max">The maximal value of segment.</param>
    public Range(T min, T max)
    {
      Min = min;
      Max = max;

#if DEBUG
      if (min is IComparable)
      {
        IComparable c1 = (IComparable)min;
        IComparable c2 = (IComparable)max;

        DebugVerify.Is(c1.CompareTo(c2) <= 0);
      }
#endif
    }
    /// <summary>
    /// Gets the minimal value of segment.
    /// </summary>
    /// <value>The min.</value>
    public T Min { get; }
    /// <summary>
    /// Gets the maximal value of segment.
    /// </summary>
    /// <value>The max.</value>
    public T Max { get; }

#pragma warning disable CS0414 // The field 'Range<T>.floatEps' is assigned but its value is never used
    static float floatEps = (float)1e-10;
#pragma warning restore CS0414 // The field 'Range<T>.floatEps' is assigned but its value is never used
#pragma warning disable CS0414 // The field 'Range<T>.doubleEps' is assigned but its value is never used
    static double doubleEps = 1e-10;
#pragma warning restore CS0414 // The field 'Range<T>.doubleEps' is assigned but its value is never used


    public static bool operator ==(Range<T> first, Range<T> second) => (first.Min.Equals(second.Min) && first.Max.Equals(second.Max) || first.IsEmpty && second.IsEmpty);
    public static bool operator !=(Range<T> first, Range<T> second) => !(first == second);
    public static bool EqualEps(Range<double> first, Range<double> second, double eps)
    {
      double delta = Math.Min(first.GetLength(), second.GetLength());
      return Abs(first.Min - second.Min) < eps * delta && Abs(first.Max - second.Max) < eps * delta;
    }

    /// <summary>
    /// Indicates whether this instance and a specified object are equal.
    /// </summary>
    /// <param name="obj">Another object to compare to.</param>
    /// <returns>
    /// true if <paramref name="obj"/> and this instance are the same type and represent the same value; otherwise, false.
    /// </returns>
    public override bool Equals(object obj)
    {
      if (obj is Range<T>)
      {
        Range<T> other = (Range<T>)obj;
        return (Min.Equals(other.Min) && Max.Equals(other.Max) || IsEmpty && other.IsEmpty);
      }
      else
      {
        return false;
      }
    }

    /// <summary>
    /// Returns the hash code for this instance.
    /// </summary>
    /// <returns>
    /// A 32-bit signed integer that is the hash code for this instance.
    /// </returns>
    public override int GetHashCode() => Min.GetHashCode() ^ Max.GetHashCode();

    /// <summary>
    /// Returns the fully qualified type name of this instance.
    /// </summary>
    /// <returns>
    /// A <see cref="T:System.String"/> containing a fully qualified type name.
    /// </returns>
    public override string ToString() => $"{Min} — {Max}";

    /// <summary>
    /// Gets a value indicating whether this range is empty.
    /// </summary>
    /// <value><c>true</c> if this instance is empty; otherwise, <c>false</c>.</value>
    public bool IsEmpty => typeof(T) is IComparable ? ((IComparable)Min).CompareTo(Max) >= 0 : Min.Equals(Max);

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>
    /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
    /// </returns>
    public bool Equals(Range<T> other) => this == other;
  }
}
