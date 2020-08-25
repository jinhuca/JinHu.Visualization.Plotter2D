using JinHu.Visualization.Plotter2D.Charts;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace JinHu.Visualization.Plotter2D
{
	/// <summary>
	/// Chart plotter is a plotter that renders axis and grid
	/// </summary>
	public class Plotter : Plotter2D
	{
		private GeneralAxis horizontalAxis = new HorizontalAxis();
		private GeneralAxis verticalAxis = new VerticalAxis();

		public Legend Legend { get; set; } = new Legend();

		public ItemsPanelTemplate LegendPanelTemplate
		{
			get => Legend.ItemsPanel;
			set => Legend.ItemsPanel = value;
		}

		public Style LegendStyle
		{
			get => Legend.Style;
			set => Legend.Style = value;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Plotter"/> class.
		/// </summary>
		public Plotter()
		{
			horizontalAxis.TicksChanged += OnHorizontalAxisTicksChanged;
			verticalAxis.TicksChanged += OnVerticalAxisTicksChanged;

			SetIsDefaultAxis(horizontalAxis as DependencyObject, true);
			SetIsDefaultAxis(verticalAxis as DependencyObject, true);

			_mouseNavigation = new MouseNavigation();
			_keyboardNavigation = new KeyboardNavigation();
			_defaultContextMenu = new DefaultContextMenu();
			horizontalAxisNavigation = new AxisNavigation { Placement = AxisPlacement.Bottom };
			verticalAxisNavigation = new AxisNavigation { Placement = AxisPlacement.Left };

			Children.AddMany(
				horizontalAxis,
				verticalAxis,
				AxisGrid,
				_mouseNavigation,
				_keyboardNavigation,
				_defaultContextMenu,
				horizontalAxisNavigation,
				verticalAxisNavigation,
				new LongOperationsIndicator(),
				Legend);

#if DEBUG
			Children.Add(new DebugMenu());
#endif

			SetAllChildrenAsDefault();
		}

		/// <summary>
		///   Creates generic plotter from this Plotter.
		/// </summary>
		/// <returns></returns>
		public GenericChartPlotter<double, double> GetGenericPlotter() => new GenericChartPlotter<double, double>(this);

		/// <summary>
		///   Creates generic plotter from this Plotter.
		///   Horizontal and Vertical types of GenericPlotter should correspond to Plotter's actual main axes types.
		/// </summary>
		/// <typeparam name="THorizontal">
		///   The type of horizontal values.
		/// </typeparam>
		/// <typeparam name="TVertical">
		///   The type of vertical values.
		/// </typeparam>
		/// <returns>
		///   GenericChartPlotter, associated to this Plotter.
		/// </returns>
		public GenericChartPlotter<THorizontal, TVertical> GetGenericPlotter<THorizontal, TVertical>() => new GenericChartPlotter<THorizontal, TVertical>(this);

		/// <summary>
		///   Creates generic plotter from this Plotter.
		/// </summary>
		/// <typeparam name="THorizontal">
		///   The type of the horizontal axis.
		/// </typeparam>
		/// <typeparam name="TVertical">
		///   The type of the vertical axis.
		/// </typeparam>
		/// <param name="horizontalAxis">
		///   The horizontal axis to use as data conversion source.
		/// </param>
		/// <param name="verticalAxis">
		///   The vertical axis to use as data conversion source.
		/// </param>
		/// <returns>
		///   GenericChartPlotter, associated to this Plotter.
		/// </returns>
		public GenericChartPlotter<THorizontal, TVertical> GetGenericPlotter<THorizontal, TVertical>(AxisBase<THorizontal> horizontalAxis, AxisBase<TVertical> verticalAxis) => new GenericChartPlotter<THorizontal, TVertical>(this, horizontalAxis, verticalAxis);

		protected Plotter(PlotterLoadMode loadMode) : base(loadMode) { }

		/// <summary>
		///   Creates empty plotter without any axes, navigation, etc.
		/// </summary>
		/// <returns>
		///   Empty plotter without any axes, navigation, etc.
		/// </returns>
		public static Plotter CreateEmpty() => new Plotter(PlotterLoadMode.OnlyViewport);

		public void BeginLongOperation() => LongOperationsIndicator.BeginLongOperation(this);

		public void EndLongOperation() => LongOperationsIndicator.EndLongOperation(this);

		#region Default charts

		private readonly MouseNavigation _mouseNavigation;
		/// <summary>
		/// Gets the default mouse navigation of Plotter.
		/// </summary>
		/// <value>The mouse navigation.</value>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public MouseNavigation MouseNavigation => _mouseNavigation;

		private readonly KeyboardNavigation _keyboardNavigation;
		/// <summary>
		/// Gets the default keyboard navigation of Plotter.
		/// </summary>
		/// <value>The keyboard navigation.</value>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public KeyboardNavigation KeyboardNavigation => _keyboardNavigation;

		private readonly DefaultContextMenu _defaultContextMenu;
		/// <summary>
		/// Gets the default context menu of Plotter.
		/// </summary>
		/// <value>The default context menu.</value>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DefaultContextMenu DefaultContextMenu => _defaultContextMenu;

		private AxisNavigation horizontalAxisNavigation;
		/// <summary>
		/// Gets the default horizontal axis navigation of Plotter.
		/// </summary>
		/// <value>The horizontal axis navigation.</value>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public AxisNavigation HorizontalAxisNavigation => horizontalAxisNavigation;

		private AxisNavigation verticalAxisNavigation;
		/// <summary>
		/// Gets the default vertical axis navigation of Plotter.
		/// </summary>
		/// <value>The vertical axis navigation.</value>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public AxisNavigation VerticalAxisNavigation => verticalAxisNavigation;

		/// <summary>
		/// Gets the default axis grid of Plotter.
		/// </summary>
		/// <value>The axis grid.</value>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public AxisGrid AxisGrid { get; } = new AxisGrid();

		#endregion

		private void OnHorizontalAxisTicksChanged(object sender, EventArgs e)
		{
			GeneralAxis axis = (GeneralAxis)sender;
			UpdateHorizontalTicks(axis);
		}

		private void UpdateHorizontalTicks(GeneralAxis axis)
		{
			AxisGrid.BeginTicksUpdate();

			if (axis != null)
			{
				AxisGrid.HorizontalTicks = axis.ScreenTicks;
				AxisGrid.MinorHorizontalTicks = axis.MinorScreenTicks;
			}
			else
			{
				AxisGrid.HorizontalTicks = null;
				AxisGrid.MinorHorizontalTicks = null;
			}

			AxisGrid.EndTicksUpdate();
		}

		private void OnVerticalAxisTicksChanged(object sender, EventArgs e)
		{
			GeneralAxis axis = (GeneralAxis)sender;
			UpdateVerticalTicks(axis);
		}

		private void UpdateVerticalTicks(GeneralAxis axis)
		{
			AxisGrid.BeginTicksUpdate();

			if (axis != null)
			{
				AxisGrid.VerticalTicks = axis.ScreenTicks;
				AxisGrid.MinorVerticalTicks = axis.MinorScreenTicks;
			}
			else
			{
				AxisGrid.VerticalTicks = null;
				AxisGrid.MinorVerticalTicks = null;
			}

			AxisGrid.EndTicksUpdate();
		}

		bool _keepOldAxis = false;
		bool _updatingAxis = false;

		/// <summary>
		/// Gets or sets the main vertical axis of Plotter.
		/// Main vertical axis of Plotter is axis which ticks are used to draw horizontal lines on AxisGrid.
		/// Value can be set to null to completely remove main vertical axis.
		/// </summary>
		/// <value>The main vertical axis.</value>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public GeneralAxis MainVerticalAxis
		{
			get => verticalAxis;
			set
			{
				if (_updatingAxis)
				{
					return;
				}

				if (value == null && verticalAxis != null)
				{
					if (!_keepOldAxis)
					{
						Children.Remove(verticalAxis);
					}
					verticalAxis.TicksChanged -= OnVerticalAxisTicksChanged;
					verticalAxis = null;
					UpdateVerticalTicks(verticalAxis);
					return;
				}

				VerifyAxisType(value.Placement, AxisType.Vertical);

				if (value != verticalAxis)
				{
					ValidateVerticalAxis(value);
					_updatingAxis = true;
					if (verticalAxis != null)
					{
						verticalAxis.TicksChanged -= OnVerticalAxisTicksChanged;
						SetIsDefaultAxis(verticalAxis, false);
						if (!_keepOldAxis)
						{
							Children.Remove(verticalAxis);
						}
						value.Visibility = verticalAxis.Visibility;
					}
					SetIsDefaultAxis(value, true);
					verticalAxis = value;
					verticalAxis.TicksChanged += OnVerticalAxisTicksChanged;

					if (!Children.Contains(value))
					{
						Children.Add(value);
					}

					UpdateVerticalTicks(value);
					OnVerticalAxisChanged();
					_updatingAxis = false;
				}
			}
		}

		protected virtual void OnVerticalAxisChanged() { }
		protected virtual void ValidateVerticalAxis(GeneralAxis axis) { }

		/// <summary>
		///   Gets or sets the main horizontal axis visibility.
		/// </summary>
		/// <value>
		///   The main horizontal axis visibility.
		/// </value>
		public Visibility MainHorizontalAxisVisibility
		{
			get { return MainHorizontalAxis?.Visibility ?? Visibility.Hidden; }
			set
			{
				if (MainHorizontalAxis != null)
				{
					MainHorizontalAxis.Visibility = value;
				}
			}
		}

		/// <summary>
		///   Gets or sets the main vertical axis visibility.
		/// </summary>
		/// <value>
		///   The main vertical axis visibility.
		/// </value>
		public Visibility MainVerticalAxisVisibility
		{
			get { return MainVerticalAxis?.Visibility ?? Visibility.Hidden; }
			set
			{
				if (MainVerticalAxis != null)
				{
					MainVerticalAxis.Visibility = value;
				}
			}
		}

		/// <summary>
		///   Gets or sets the main horizontal axis of Plotter.
		///   Main horizontal axis of Plotter is axis which ticks are used to draw vertical lines on AxisGrid.
		///   Value can be set to null to completely remove main horizontal axis.
		/// </summary>
		/// <value>
		///   The main horizontal axis.
		/// </value>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public GeneralAxis MainHorizontalAxis
		{
			get => horizontalAxis;
			set
			{
				if (_updatingAxis)
				{
					return;
				}

				if (value == null && horizontalAxis != null)
				{
					Children.Remove(horizontalAxis);
					horizontalAxis.TicksChanged -= OnHorizontalAxisTicksChanged;
					horizontalAxis = null;
					UpdateHorizontalTicks(horizontalAxis);
					return;
				}

				VerifyAxisType(value.Placement, AxisType.Horizontal);

				if (value != horizontalAxis)
				{
					ValidateHorizontalAxis(value);
					_updatingAxis = true;
					if (horizontalAxis != null)
					{
						horizontalAxis.TicksChanged -= OnHorizontalAxisTicksChanged;
						SetIsDefaultAxis(horizontalAxis, false);
						if (!_keepOldAxis)
						{
							Children.Remove(horizontalAxis);
						}
						value.Visibility = horizontalAxis.Visibility;
					}
					SetIsDefaultAxis(value, true);
					horizontalAxis = value;
					horizontalAxis.TicksChanged += OnHorizontalAxisTicksChanged;

					if (!Children.Contains(value))
					{
						Children.Add(value);
					}

					UpdateHorizontalTicks(value);
					OnHorizontalAxisChanged();
					_updatingAxis = false;
				}
			}
		}

		protected virtual void OnHorizontalAxisChanged() { }
		protected virtual void ValidateHorizontalAxis(GeneralAxis axis) { }

		private static void VerifyAxisType(AxisPlacement axisPlacement, AxisType axisType)
		{
			var result = false;
			switch (axisPlacement)
			{
				case AxisPlacement.Left:
				case AxisPlacement.Right:
					result = axisType == AxisType.Vertical;
					break;
				case AxisPlacement.Top:
				case AxisPlacement.Bottom:
					result = axisType == AxisType.Horizontal;
					break;
			}

			if (!result)
			{
				throw new ArgumentException(Strings.Exceptions.InvalidAxisPlacement);
			}
		}

		protected override void OnIsDefaultAxisChangedCore(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var axis = d as GeneralAxis;
			if (axis != null)
			{
				var value = (bool)e.NewValue;
				var oldKeepOldAxis = _keepOldAxis;
				var horizontal = axis.Placement == AxisPlacement.Bottom || axis.Placement == AxisPlacement.Top;
				_keepOldAxis = true;

				if (value && horizontal)
				{
					MainHorizontalAxis = axis;
				}
				else if (value && !horizontal)
				{
					MainVerticalAxis = axis;
				}
				else if (!value && horizontal)
				{
					MainHorizontalAxis = null;
				}
				else if (!value && !horizontal)
				{
					MainVerticalAxis = null;
				}

				_keepOldAxis = oldKeepOldAxis;
			}
		}

		public bool NewLegendVisible
		{
			get => Legend.LegendVisible;
			set => Legend.LegendVisible = value;
		}

		private enum AxisType
		{
			Horizontal,
			Vertical
		}
	}
}