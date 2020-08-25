using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace JinHu.Visualization.Plotter2D
{
	/// <summary>
	///   Control for plotting 2d images
	/// </summary>
	public class Plotter2D : PlotterBase
	{
		/// <summary>
		///   Initializes a new instance of the <see cref="Plotter2D"/> class.
		/// </summary>
		public Plotter2D() : base(PlotterLoadMode.Normal)
		{
			Children.CollectionChanged += (s, e) => viewport.UpdateIterationCount = 0;
			InitViewport();
		}

		private void InitViewport()
		{
			ViewportPanel = new Canvas();
			Grid.SetColumn(ViewportPanel, 1);
			Grid.SetRow(ViewportPanel, 1);

			viewport = new Viewport2D(ViewportPanel, this);
			if (LoadMode != PlotterLoadMode.Empty)
			{
				MainGrid.Children.Add(ViewportPanel);
			}
		}

		protected Plotter2D(PlotterLoadMode loadMode) : base(loadMode)
		{
			if (loadMode != PlotterLoadMode.Empty)
			{
				InitViewport();
			}
		}

		/// <summary>
		/// Gets or sets the panel, which contains viewport.
		/// </summary>
		/// <value>
		/// The viewport panel.
		/// </value>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Panel ViewportPanel { get; protected set; }

		private Viewport2D viewport;
		private Viewport2dDeferredPanningProxy deferredPanningProxy;

		protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
		{
			// This is part of endless axis resize loop workaround
			if (viewport != null)
			{
				viewport.UpdateIterationCount = 0;
				if (!viewport.EnforceRestrictions)
				{
					Debug.WriteLine("Plotter: enabling viewport constraints");
					viewport.EnforceRestrictions = true;
				}
			}
			base.OnRenderSizeChanged(sizeInfo);
		}

		/// <summary>
		///   Gets the viewport.
		/// </summary>
		/// <value>
		///   The viewport.
		/// </value>
		[NotNull]
		public Viewport2D Viewport
		{
			get
			{
				bool useDeferredPanning = false;
				if (CurrentChild != null)
				{
					DependencyObject dependencyChild = CurrentChild as DependencyObject;
					if (dependencyChild != null)
					{
						useDeferredPanning = Viewport2D.GetUseDeferredPanning(dependencyChild);
					}
				}

				if (useDeferredPanning)
				{
					if (deferredPanningProxy == null)
					{
						deferredPanningProxy = new Viewport2dDeferredPanningProxy(viewport);
					}
					return deferredPanningProxy;
				}

				return viewport;
			}
			protected set { viewport = value; }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public double ViewportClipToBoundsEnlargeFactor
		{
			get => viewport.ClipToBoundsEnlargeFactor;
			set => viewport.ClipToBoundsEnlargeFactor = value;
		}

		/// <summary>
		///   Gets or sets the data transform.
		/// </summary>
		/// <value>
		///   The data transform.
		/// </value>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DataTransform DataTransform
		{
			get => viewport.Transform.DataTransform;
			set => viewport.Transform = viewport.Transform.WithDataTransform(value);
		}

		/// <summary>
		///   Gets or sets the transform.
		/// </summary>
		/// <value>
		///   The transform.
		/// </value>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public CoordinateTransform Transform
		{
			get => viewport.Transform;
			set => viewport.Transform = value;
		}

		/// <summary>
		///   Fits to view.
		/// </summary>
		public void FitToView() => viewport.FitToView();

		/// <summary>
		///   Gets or sets the visible area rectangle.
		/// </summary>
		/// <value>
		///   The visible.
		/// </value>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DataRect Visible
		{
			get => viewport.Visible;
			set => viewport.Visible = value;
		}

		/// <summary>
		///   Gets or sets the domain - maximal value of visible area.
		/// </summary>
		/// <value>
		///   The domain.
		/// </value>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DataRect Domain
		{
			get => viewport.Domain;
			set => viewport.Domain = value;
		}

		/// <summary>
		///   Gets the constraints being applied to viewport.
		/// </summary>
		/// <value>
		///   The constraints.
		/// </value>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ConstraintCollection Constraints => viewport.Constraints;

		/// <summary>
		///   Gets the fit to view constraints being applied to viewport in 'fit to view' state.
		/// </summary>
		/// <value>
		///   The fit to view constraints.
		/// </value>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ConstraintCollection FitToViewConstraints => viewport.FitToViewConstraints;
	}
}
