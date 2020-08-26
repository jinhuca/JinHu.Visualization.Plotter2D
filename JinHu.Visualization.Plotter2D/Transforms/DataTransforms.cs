﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace JinHu.Visualization.Plotter2D
{
  /// <summary>
  /// Base class for all data transforms.
  /// Defines methods to transform point from data coordinate system to viewport coordinates and vice versa.
  /// Derived class should be immutable; to perform any changes a new new instance with different parameters should be created.
  /// </summary>
  public abstract class DataTransform
  {
    /// <summary>
    /// Transforms the point in data coordinates to viewport coordinates.
    /// </summary>
    /// <param name="pt">The point in data coordinates.</param>
    /// <returns>Transformed point in viewport coordinates.</returns>
    public abstract Point DataToViewport(Point pt);

    /// <summary>
    /// Transforms the point in viewport coordinates to data coordinates.
    /// </summary>
    /// <param name="pt">The point in viewport coordinates.</param>
    /// <returns>Transformed point in data coordinates.</returns>
    public abstract Point ViewportToData(Point pt);

    /// <summary>
    /// Gets the data domain of this dataTransform.
    /// </summary>
    /// <value>The data domain of this dataTransform.</value>
    public virtual DataRect DataDomain => DefaultDomain;

    public static DataRect DefaultDomain { get; } = DataRect.Empty;
  }

  /// <summary>
  /// Represents identity data transform, which applies no transformation.
  /// is by default in CoordinateTransform.
  /// </summary>
  public sealed class IdentityTransform : DataTransform
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="IdentityTransform"/> class.
    /// </summary>
    public IdentityTransform() { }

    /// <summary>
    /// Transforms the point in data coordinates to viewport coordinates.
    /// </summary>
    /// <param name="pt">The point in data coordinates.</param>
    /// <returns></returns>
    public override Point DataToViewport(Point pt) => pt;

    /// <summary>
    /// Transforms the point in viewport coordinates to data coordinates.
    /// </summary>
    /// <param name="pt">The point in viewport coordinates.</param>
    /// <returns></returns>
    public override Point ViewportToData(Point pt) => pt;
  }

  /// <summary>
  /// Represents a logarithmic transform of y-values of points.
  /// </summary>
  public sealed class Log10YTransform : DataTransform
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Log10YTransform"/> class.
    /// </summary>
    public Log10YTransform() { }

    /// <summary>
    /// Transforms the point in data coordinates to viewport coordinates.
    /// </summary>
    /// <param name="pt">The point in data coordinates.</param>
    /// <returns></returns>
    public override Point DataToViewport(Point pt)
    {
      double y = pt.Y;

      if (y < 0)
      {
        y = double.MinValue;
      }
      else
      {
        y = Math.Log10(y);
      }

      return new Point(pt.X, y);
    }

    /// <summary>
    /// Transforms the point in viewport coordinates to data coordinates.
    /// </summary>
    /// <param name="pt">The point in viewport coordinates.</param>
    /// <returns></returns>
    public override Point ViewportToData(Point pt) => new Point(pt.X, Math.Pow(10, pt.Y));

    /// <summary>
    /// Gets the data domain of this dataTransform.
    /// </summary>
    /// <value>The data domain of this dataTransform.</value>
    public override DataRect DataDomain => DataDomains.YPositive;
  }

  /// <summary>
  /// Represents a logarithmic transform of x-values of points.
  /// </summary>
  public sealed class Log10XTransform : DataTransform
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Log10XTransform"/> class.
    /// </summary>
    public Log10XTransform() { }

    /// <summary>
    /// Transforms the point in data coordinates to viewport coordinates.
    /// </summary>
    /// <param name="pt">The point in data coordinates.</param>
    /// <returns></returns>
    public override Point DataToViewport(Point pt)
    {
      double x = pt.X;

      if (x < 0)
      {
        x = double.MinValue;
      }
      else
      {
        x = Math.Log10(x);
      }

      return new Point(x, pt.Y);
    }

    /// <summary>
    /// Transforms the point in viewport coordinates to data coordinates.
    /// </summary>
    /// <param name="pt">The point in viewport coordinates.</param>
    /// <returns></returns>
    public override Point ViewportToData(Point pt) => new Point(Math.Pow(10, pt.X), pt.Y);

    /// <summary>
    /// Gets the data domain.
    /// </summary>
    /// <value>The data domain.</value>
    public override DataRect DataDomain => DataDomains.XPositive;
  }

  /// <summary>
  /// Represents a mercator transform, used in maps.
  /// Transforms y coordinates.
  /// </summary>
  public sealed class MercatorTransform : DataTransform
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="MercatorTransform"/> class.
    /// </summary>
    public MercatorTransform() => CalcScale(MaxLatitude);

    /// <summary>
    /// Initializes a new instance of the <see cref="MercatorTransform"/> class.
    /// </summary>
    /// <param name="maxLatitude">The maximal latitude.</param>
    public MercatorTransform(double maxLatitude)
    {
      MaxLatitude = maxLatitude;
      CalcScale(maxLatitude);
    }

    private void CalcScale(double maxLatitude)
    {
      double maxLatDeg = maxLatitude;
      double maxLatRad = maxLatDeg * Math.PI / 180;
      Scale = maxLatDeg / Math.Log(Math.Tan(maxLatRad / 2 + Math.PI / 4));
    }
    /// <summary>
    /// Gets the scale.
    /// </summary>
    /// <value>The scale.</value>
    public double Scale { get; private set; }

    /// <summary>
    /// Gets the maximal latitude.
    /// </summary>
    /// <value>The max latitude.</value>
    public double MaxLatitude { get; } = 85;

    /// <summary>
    /// Transforms the point in data coordinates to viewport coordinates.
    /// </summary>
    /// <param name="pt">The point in data coordinates.</param>
    /// <returns></returns>
    public sealed override Point DataToViewport(Point pt)
    {
      double y = pt.Y;
      if (-MaxLatitude <= y && y <= MaxLatitude)
      {
        y = Scale * Math.Log(Math.Tan(Math.PI * (pt.Y + 90) / 360));
      }

      return new Point(pt.X, y);
    }

    /// <summary>
    /// Transforms the point in viewport coordinates to data coordinates.
    /// </summary>
    /// <param name="pt">The point in viewport coordinates.</param>
    /// <returns></returns>
    public sealed override Point ViewportToData(Point pt)
    {
      double y = pt.Y;
      if (-MaxLatitude <= y && y <= MaxLatitude)
      {
        double e = Math.Exp(y / Scale);
        y = 360 * Math.Atan(e) / Math.PI - 90;
      }

      return new Point(pt.X, y);
    }
  }

  /// <summary>
  /// Represents transform from polar coordinate system to rectangular coordinate system.
  /// </summary>
  public sealed class PolarToRectTransform : DataTransform
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="PolarToRectTransform"/> class.
    /// </summary>
    public PolarToRectTransform() { }

    /// <summary>
    /// Transforms the point in data coordinates to viewport coordinates.
    /// </summary>
    /// <param name="pt">The point in data coordinates.</param>
    /// <returns></returns>
    public override Point DataToViewport(Point pt)
    {
      double r = pt.X;
      double phi = pt.Y;

      double x = r * Math.Cos(phi);
      double y = r * Math.Sin(phi);

      return new Point(x, y);
    }

    /// <summary>
    /// Transforms the point in viewport coordinates to data coordinates.
    /// </summary>
    /// <param name="pt">The point in viewport coordinates.</param>
    /// <returns></returns>
    public override Point ViewportToData(Point pt)
    {
      double x = pt.X;
      double y = pt.Y;
      double r = Math.Sqrt(x * x + y * y);
      double phi = Math.Atan2(y, x);

      return new Point(r, phi);
    }
  }

  /// <summary>
  /// Represents a data transform which applies rotation around specified center at specified angle.
  /// </summary>
  public sealed class RotateDataTransform : DataTransform
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="RotateDataTransform"/> class.
    /// </summary>
    /// <param name="angleInRadians">The angle in radians.</param>
    public RotateDataTransform(double angleInRadians)
    {
      Center = new Point();
      Angle = angleInRadians;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RotateDataTransform"/> class.
    /// </summary>
    /// <param name="angleInRadians">The angle in radians.</param>
    /// <param name="center">The center of rotation.</param>
    public RotateDataTransform(double angleInRadians, Point center)
    {
      Center = center;
      Angle = angleInRadians;
    }
    /// <summary>
    /// Gets the center of rotation.
    /// </summary>
    /// <value>The center.</value>
    public Point Center { get; }

    /// <summary>
    /// Gets the rotation angle.
    /// </summary>
    /// <value>The angle.</value>
    public double Angle { get; }

    /// <summary>
    /// Transforms the point in data coordinates to viewport coordinates.
    /// </summary>
    /// <param name="pt">The point in data coordinates.</param>
    /// <returns></returns>
    public override Point DataToViewport(Point pt) => Transform(pt, Angle);

    /// <summary>
    /// Transforms the point in viewport coordinates to data coordinates.
    /// </summary>
    /// <param name="pt">The point in viewport coordinates.</param>
    /// <returns></returns>
    public override Point ViewportToData(Point pt) => Transform(pt, -Angle);

    private Point Transform(Point pt, double angle)
    {
      Vector vec = pt - Center;
      double currAngle = Math.Atan2(vec.Y, vec.X);
      currAngle += angle;
      Vector rotatedVec = new Vector(Math.Cos(currAngle), Math.Sin(currAngle)) * vec.Length;
      return Center + rotatedVec;
    }
  }

  /// <summary>
  /// Represents data transform performed by multiplication on given matrix.
  /// </summary>
  public sealed class MatrixDataTransform : DataTransform
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="MatrixDataTransform"/> class.
    /// </summary>
    /// <param name="matrix">The transform matrix.</param>
    public MatrixDataTransform(Matrix matrix)
    {
      Matrix = matrix;
      InvertedMatrix = matrix;
      InvertedMatrix.Invert();
    }

    public Matrix Matrix { get; }

    public Matrix InvertedMatrix { get; }

    /// <summary>
    /// Transforms the point in data coordinates to viewport coordinates.
    /// </summary>
    /// <param name="pt">The point in data coordinates.</param>
    /// <returns></returns>
    public override Point DataToViewport(Point pt) => Matrix.Transform(pt);

    /// <summary>
    /// Transforms the point in viewport coordinates to data coordinates.
    /// </summary>
    /// <param name="pt">The point in viewport coordinates.</param>
    /// <returns></returns>
    public override Point ViewportToData(Point pt) => InvertedMatrix.Transform(pt);
  }

  /// <summary>
  /// Represents a chain of transforms which are being applied consequently.
  /// </summary>
  public sealed class CompositeDataTransform : DataTransform
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="CompositeDataTransform"/> class.
    /// </summary>
    /// <param name="transforms">The transforms.</param>
    public CompositeDataTransform(params DataTransform[] transforms)
    {
      if (transforms == null)
      {
        throw new ArgumentNullException("transforms");
      }

      foreach (var transform in transforms)
      {
        if (transform == null)
        {
          throw new ArgumentNullException("transforms", Strings.Exceptions.EachTransformShouldNotBeNull);
        }
      }

      Transforms = transforms;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CompositeDataTransform"/> class.
    /// </summary>
    /// <param name="transforms">The transforms.</param>
    public CompositeDataTransform(IEnumerable<DataTransform> transforms)
    {
      Transforms = transforms ?? throw new ArgumentNullException("transforms");
    }

    /// <summary>
    /// Gets the transforms.
    /// </summary>
    /// <value>The transforms.</value>
    public IEnumerable<DataTransform> Transforms { get; }

    /// <summary>
    /// Transforms the point in data coordinates to viewport coordinates.
    /// </summary>
    /// <param name="pt">The point in data coordinates.</param>
    /// <returns></returns>
    public override Point DataToViewport(Point pt)
    {
      foreach (var transform in Transforms)
      {
        pt = transform.DataToViewport(pt);
      }

      return pt;
    }

    /// <summary>
    /// Transforms the point in viewport coordinates to data coordinates.
    /// </summary>
    /// <param name="pt">The point in viewport coordinates.</param>
    /// <returns></returns>
    public override Point ViewportToData(Point pt)
    {
      foreach (var transform in Transforms.Reverse())
      {
        pt = transform.ViewportToData(pt);
      }

      return pt;
    }
  }

  /// <summary>
  /// Represents a data transform, performed by given lambda function.
  /// </summary>
  public sealed class LambdaDataTransform : DataTransform
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="DelegateDataTransform"/> class.
    /// </summary>
    /// <param name="dataToViewport">The data to viewport transform delegate.</param>
    /// <param name="viewportToData">The viewport to data transform delegate.</param>
    public LambdaDataTransform(Func<Point, Point> dataToViewport, Func<Point, Point> viewportToData)
    {
      DataToViewportFunc = dataToViewport ?? throw new ArgumentNullException("dataToViewport");
      ViewportToDataFunc = viewportToData ?? throw new ArgumentNullException("viewportToData");
    }
    /// <summary>
    /// Gets the data to viewport transform delegate.
    /// </summary>
    /// <value>The data to viewport func.</value>
    public Func<Point, Point> DataToViewportFunc { get; }

    /// <summary>
    /// Gets the viewport to data transform delegate.
    /// </summary>
    /// <value>The viewport to data func.</value>
    public Func<Point, Point> ViewportToDataFunc { get; }

    /// <summary>
    /// Transforms the point in data coordinates to viewport coordinates.
    /// </summary>
    /// <param name="pt">The point in data coordinates.</param>
    /// <returns></returns>
    public override Point DataToViewport(Point pt) => DataToViewportFunc(pt);

    /// <summary>
    /// Transforms the point in viewport coordinates to data coordinates.
    /// </summary>
    /// <param name="pt">The point in viewport coordinates.</param>
    /// <returns></returns>
    public override Point ViewportToData(Point pt) => ViewportToDataFunc(pt);
  }

  /// <summary>
  /// Contains default data transforms.
  /// </summary>
  public static class DataTransforms
  {
    /// <summary>
    /// Gets the default identity data transform.
    /// </summary>
    /// <value>The identity data transform.</value>
    public static IdentityTransform Identity { get; } = new IdentityTransform();
  }
}
