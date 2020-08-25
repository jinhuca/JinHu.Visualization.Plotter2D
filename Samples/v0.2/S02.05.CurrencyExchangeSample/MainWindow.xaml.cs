using JinHu.Visualization.Plotter2D;
using JinHu.Visualization.Plotter2D.DataSources;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace S02.CurrencyExchangeSample
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    private List<CurrencyInfo> usd;
    private List<CurrencyInfo> eur;
    private List<CurrencyInfo> gbp;
    private List<CurrencyInfo> jpy;

    public MainWindow()
    {
      InitializeComponent();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
      usd = LoadCurrencyRates("usd.txt");
      eur = LoadCurrencyRates("eur.txt");
      gbp = LoadCurrencyRates("gbp.txt");
      jpy = LoadCurrencyRates("jpy.txt");

      Color[] colors = ColorHelper.CreateRandomColors(4);
      plotter.AddLineGraph(CreateCurrencyDataSource(usd), colors[0], 1, "RUB / USD");
      plotter.AddLineGraph(CreateCurrencyDataSource(eur), colors[1], 1, "RUB / EURO");
      plotter.AddLineGraph(CreateCurrencyDataSource(gbp), colors[2], 1, "RUB / GBP");
      plotter.AddLineGraph(CreateCurrencyDataSource(jpy), colors[3], 1, "RUB / JPY");
    }

    private static List<CurrencyInfo> LoadCurrencyRates(string fileName)
    {
      string[] strings = File.ReadAllLines(fileName);
      var res = new List<CurrencyInfo>(strings.Length - 1);
      for (int i = 1; i < strings.Length; i++)
      {
        string line = strings[i];
        string[] subLines = line.Split('\t');

        DateTime date = DateTime.Parse(subLines[1]);
        double rate = double.Parse(subLines[5], CultureInfo.InvariantCulture);

        res.Add(new CurrencyInfo { Date = date, Rate = rate });
      }

      return res;
    }

    private EnumerableDataSource<CurrencyInfo> CreateCurrencyDataSource(List<CurrencyInfo> rates)
    {
      var ds = new EnumerableDataSource<CurrencyInfo>(rates);
      ds.SetXMapping(ci => dateAxis.ConvertToDouble(ci.Date));
      ds.SetYMapping(ci => ci.Rate);
      return ds;
    }
  }
}
