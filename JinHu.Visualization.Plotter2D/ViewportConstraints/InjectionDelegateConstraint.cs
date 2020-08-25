using System;

namespace JinHu.Visualization.Plotter2D
{
  public delegate DataRect ViewportConstraintCallback(DataRect proposedDataRect);

  public class InjectionDelegateConstraint : ViewportConstraint
  {
    public InjectionDelegateConstraint(Viewport2D masterViewport, ViewportConstraintCallback callback)
    {
      Callback = callback ?? throw new ArgumentNullException("callback");
      MasterViewport = masterViewport ?? throw new ArgumentNullException("masterViewport");
      masterViewport.PropertyChanged += MasterViewport_PropertyChanged;
    }

    void MasterViewport_PropertyChanged(object sender, ExtendedPropertyChangedEventArgs e)
    {
      if (e.PropertyName == "Visible")
      {
        RaiseChanged();
      }
    }

    public ViewportConstraintCallback Callback { get; set; }
    public Viewport2D MasterViewport { get; set; }

    public override DataRect Apply(DataRect previousDataRect, DataRect proposedDataRect, Viewport2D viewport)
    {
      return Callback(proposedDataRect);
    }
  }
}
