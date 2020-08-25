using System.ComponentModel;
using System.Windows;

namespace JinHu.Visualization.Plotter2D
{
	/// <summary>
	/// One of the simplest implementations of IPlotterElement interface.
	/// </summary>
	public abstract class PlotterElement : FrameworkElement, IPlotterElement
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PlotterElement"/> class.
		/// </summary>
		protected PlotterElement() { }

		/// <summary>
		/// Gets the parent plotter of chart.
		/// Should be equal to null if item is not connected to any plotter.
		/// </summary>
		/// <value>The plotter.</value>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public PlotterBase Plotter { get; private set; }

		/// <summary>
		/// This method is invoked when element is attached to plotter. 
		/// It is the place to put additional controls to Plotter.
		/// </summary>
		/// <param name="plotter">Plotter for this element</param>
		protected virtual void OnPlotterAttached(PlotterBase plotter) => Plotter = plotter;

		/// <summary>
		/// This method is invoked when element is being detached from plotter. 
		/// If additionalcontrols were put on plotter in OnPlotterAttached method, they should be removed here.
		/// </summary>
		/// <remarks>
		/// This method is always called in pair with OnPlotterAttached.
		/// </remarks>
		protected virtual void OnPlotterDetaching(PlotterBase plotter) => Plotter = null;

		#region IPlotterElement Members

		void IPlotterElement.OnPlotterAttached(PlotterBase plotter) => OnPlotterAttached(plotter);
		void IPlotterElement.OnPlotterDetaching(PlotterBase plotter) => OnPlotterDetaching(plotter);

		#endregion
	}
}