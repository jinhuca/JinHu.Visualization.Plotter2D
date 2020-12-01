using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace JinHu.Visualization.Plotter2D
{
  /// <summary>
  /// ViewportElement2D is intended to be a child of Viewport2D. 
  /// Specifics of ViewportElement2D is Viewport2D attached property.
  /// </summary>
  public abstract class ViewportElement2D : FrameworkElement, IPlotterElement, INotifyPropertyChanged
  {
    protected virtual Panel GetHostPanel(PlotterBase plotter) => plotter.CentralGrid;

    #region IPlotterElement Members

    void IPlotterElement.OnPlotterAttached(PlotterBase plotter) => OnPlotterAttached(plotter);
    void IPlotterElement.OnPlotterDetaching(PlotterBase plotter) => OnPlotterDetaching(plotter);
    public PlotterBase Plotter { get; private set; }

    protected virtual void OnPlotterAttached(PlotterBase plotter)
    {
      Plotter = plotter;
      GetHostPanel(plotter).Children.Add(this);
      Viewport = Plotter.Viewport;
      Viewport.PropertyChanged += OnViewportPropertyChanged;
    }

    private void OnViewportPropertyChanged(object sender, ExtendedPropertyChangedEventArgs e)
    {
      switch (e.PropertyName)
      {
        case "Visible":
          OnVisibleChanged((DataRect)e.NewValue, (DataRect)e.OldValue);
          break;
        case "Output":
          OnOutputChanged((Rect)e.NewValue, (Rect)e.OldValue);
          break;
        case "Transform":
          Update();
          break;
      }
    }

    protected virtual void OnPlotterDetaching(PlotterBase plotter)
    {
      Viewport.PropertyChanged -= OnViewportPropertyChanged;
      Viewport = null;
      GetHostPanel(plotter).Children.Remove(this);
      Plotter = null;
    }

    #endregion IPlotterElement Members

    public int ZIndex
    {
      get => Panel.GetZIndex(this);
      set => Panel.SetZIndex(this, value);
    }

    #region Viewport

    protected Viewport2D Viewport { get; private set; }

    #endregion

    #region Description

    /// <summary>
    /// Creates the default description.
    /// </summary>
    /// <returns></returns>
    protected virtual Description CreateDefaultDescription()
    {
      return new StandardDescription();
    }

    private Description description;
    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    /// <value>The description.</value>
    public Description Description
    {
      get
      {
        if (description == null)
        {
          description = CreateDefaultDescription();
          description.Attach(this);
        }
        return description;
      }
      set
      {
        if (description != null)
        {
          description.Detach();
        }
        description = value;
        if (description != null)
        {
          description.Attach(this);
        }
        RaisePropertyChanged();
      }
    }

    public override string ToString()
    {
      return GetType().Name + ": " + Description.Brief;
    }

    #endregion

    protected internal Vector Offset { get; set; }

    //bool SizeEqual(Diameter s1, Diameter s2, double eps)
    //{
    //    double width = Math.Min(s1.Width, s2.Width);
    //    double height = Math.Min(s1.Height, s2.Height);
    //    return Math.Abs(s1.Width - s2.Width) < width * eps &&
    //           Math.Abs(s1.Height - s2.Height) < height * eps;
    //}

    protected virtual void OnVisibleChanged(DataRect newRect, DataRect oldRect)
    {
      if (newRect.Size == oldRect.Size)
      {
        var transform = Viewport.Transform;
        Offset += oldRect.Location.DataToScreen(transform) - newRect.Location.DataToScreen(transform);
        if (ManualTranslate)
        {
          Update();
        }
      }
      else
      {
        Offset = new Vector();
        Update();
      }
    }

    protected virtual void OnOutputChanged(Rect newRect, Rect oldRect)
    {
      Offset = new Vector();
      Update();
    }

    /// <summary>
    ///   Gets a value indicating whether this instance is translated.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance is translated; otherwise, <c>false</c>.
    /// </value>
    protected bool IsTranslated => Offset.X != 0 || Offset.Y != 0;

    #region IsLevel

    public bool IsLayer
    {
      get { return (bool)GetValue(IsLayerProperty); }
      set { SetValue(IsLayerProperty, value); }
    }

    public static readonly DependencyProperty IsLayerProperty = DependencyProperty.Register(
      "IsLayer",
      typeof(bool),
      typeof(ViewportElement2D),
      new FrameworkPropertyMetadata(false));

    #endregion

    #region Rendering & caching options

    protected object GetValueSync(DependencyProperty property) =>
      Dispatcher.Invoke(DispatcherPriority.Send, (DispatcherOperationCallback)delegate { return GetValue(property); }, property);

    protected void SetValueAsync(DependencyProperty property, object value)
      => Dispatcher.BeginInvoke(DispatcherPriority.Send, (SendOrPostCallback)delegate { SetValue(property, value); }, value);

    private bool manualClip;

    /// <summary>
    ///   Gets or sets a value indicating whether descendant graph class relies on autotic clipping by Viewport.Output or
    ///   does its own clipping.
    /// </summary>
    public bool ManualClip
    {
      get { return manualClip; }
      set { manualClip = value; }
    }

    private bool manualTranslate;
    /// <summary>
    ///   Gets or sets a value indicating whether descendant graph class relies on automatic translation of it, or does its own.
    /// </summary>
    public bool ManualTranslate
    {
      get { return manualTranslate; }
      set { manualTranslate = value; }
    }

    private RenderTo renderTarget = RenderTo.Screen;
    /// <summary>
    ///   Gets or sets a value indicating whether descendant graph class uses cached rendering of its content to image, or not.
    /// </summary>
    public RenderTo RenderTarget
    {
      get { return renderTarget; }
      set { renderTarget = value; }
    }

    private enum ImageKind
    {
      Real,
      BeingRendered,
      Empty
    }

    #endregion

    private RenderState CreateRenderState(DataRect renderVisible, RenderTo renderingType)
      => new RenderState(renderVisible, Viewport.Visible, Viewport.Output, renderingType);

    protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property == VisibilityProperty)
      {
        Update();
      }
    }

    private bool updateCalled;
    private bool beforeFirstUpdate = true;
    protected void Update()
    {
      if (Viewport == null)
      {
        return;
      }

      UpdateCore();
      if (!beforeFirstUpdate)
      {
        updateCalled = true;
        InvalidateVisual();
      }
      beforeFirstUpdate = false;
    }

    protected virtual void UpdateCore() { }

    protected void TranslateVisual()
    {
      if (!ManualTranslate)
      {
        shouldReRender = false;
      }
      InvalidateVisual();
    }

    #region Thumbnail

    private ImageSource thumbnail;
    public ImageSource Thumbnail
    {
      get
      {
        if (!CreateThumbnail)
        {
          CreateThumbnail = true;
        }
        return thumbnail;
      }
    }

    private bool createThumbnail;
    public bool CreateThumbnail
    {
      get { return createThumbnail; }
      set
      {
        if (createThumbnail != value)
        {
          createThumbnail = value;
          if (value)
          {
            RenderThumbnail();
          }
          else
          {
            thumbnail = null;
            RaisePropertyChanged("Thumbnail");
          }
        }
      }
    }

    private bool ShouldCreateThumbnail => IsLayer && createThumbnail;

    private void RenderThumbnail()
    {
      if (Viewport == null)
      {
        return;
      }

      Rect output = Viewport.Output;
      if (output.Width == 0 || output.Height == 0)
      {
        return;
      }

      DataRect visible = Viewport.Visible;
      var transform = Viewport.Transform;
      DrawingVisual visual = new DrawingVisual();
      using (DrawingContext dc = visual.RenderOpen())
      {
        Point outputStart = visible.Location.DataToScreen(transform);
        double x = -outputStart.X + Offset.X;
        double y = -outputStart.Y + output.Bottom - output.Top + Offset.Y;
        bool translate = !manualTranslate && IsTranslated;
        if (translate)
        {
          dc.PushTransform(new TranslateTransform(x, y));
        }

        const byte c = 240;
        Brush brush = new SolidColorBrush(Color.FromArgb(120, c, c, c));
        Pen pen = new Pen(Brushes.Black, 1);
        dc.DrawRectangle(brush, pen, output);
        dc.DrawDrawing(graphContents);

        if (translate)
        {
          dc.Pop();
        }
      }

      RenderTargetBitmap bmp = new RenderTargetBitmap((int)output.Width, (int)output.Height, 96, 96, PixelFormats.Pbgra32);
      bmp.Render(visual);
      thumbnail = bmp;
      RaisePropertyChanged("Thumbnail");
    }

    #endregion

    private bool shouldReRender = true;
    private DrawingGroup graphContents;
    protected sealed override void OnRender(DrawingContext drawingContext)
    {
      if (Viewport == null)
      {
        return;
      }

      Rect output = Viewport.Output;
      if (output.Width == 0 || output.Height == 0)
      {
        return;
      }
      if (output.IsEmpty)
      {
        return;
      }
      if (Visibility != Visibility.Visible)
      {
        return;
      }
      if (shouldReRender || manualTranslate || renderTarget == RenderTo.Image || beforeFirstUpdate || updateCalled)
      {
        if (graphContents == null)
        {
          graphContents = new DrawingGroup();
        }
        if (beforeFirstUpdate)
        {
          Update();
        }

        using (DrawingContext context = graphContents.Open())
        {
          if (renderTarget == RenderTo.Screen)
          {
            RenderState state = CreateRenderState(Viewport.Visible, RenderTo.Screen);
            OnRenderCore(context, state);
          }
        }
        updateCalled = false;
      }

      // thumbnail is not created, if
      // 1) CreateThumbnail is false
      // 2) this graph has IsLayer property, set to false
      if (ShouldCreateThumbnail)
      {
        RenderThumbnail();
      }

      if (!manualClip)
      {
        drawingContext.PushClip(new RectangleGeometry(output));
      }
      bool translate = !manualTranslate && IsTranslated;
      if (translate)
      {
        drawingContext.PushTransform(new TranslateTransform(Offset.X, Offset.Y));
      }

      drawingContext.DrawDrawing(graphContents);

      if (translate)
      {
        drawingContext.Pop();
      }
      if (!manualClip)
      {
        drawingContext.Pop();
      }
      shouldReRender = true;
    }

    protected abstract void OnRenderCore(DrawingContext dc, RenderState state);

    #region INotifyPropertyChanged Members

    public event PropertyChangedEventHandler PropertyChanged;

    protected void RaisePropertyChanged([CallerMemberName] string propertyNamme = "")
      => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyNamme));

    #endregion
  }
}