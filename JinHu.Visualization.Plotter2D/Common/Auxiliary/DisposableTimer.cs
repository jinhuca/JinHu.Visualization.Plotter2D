using System;
using System.Diagnostics;

namespace JinHu.Visualization.Plotter2D.Common
{
  public sealed class DisposableTimer : IDisposable
  {
    private readonly bool isActive = true;
    private readonly string name;
    readonly Stopwatch timer;
    public DisposableTimer(string name) : this(name, true) { }

    public DisposableTimer(string name, bool isActive)
    {
      this.name = name;
      this.isActive = isActive;
      if (isActive)
      {
        timer = Stopwatch.StartNew();
        Trace.WriteLine(name + ": started " + DateTime.Now.TimeOfDay);
      }
    }

    #region IDisposable Members

    public void Dispose()
    {
      //#if DEBUG
      if (isActive)
      {
        var duration = timer.ElapsedMilliseconds;
        Trace.WriteLine(name + ": elapsed " + duration + " ms.");
        timer.Stop();
      }
      //#endif
    }

    #endregion
  }
}
