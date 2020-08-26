using JinHu.Visualization.Plotter2D.DataSources;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows;

namespace IsolineSample
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
      Loaded += MainWindow_Loaded;
    }

    void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
      LoadField();
    }

    private void LoadField()
    {
      List<string> strings = new List<string>();
      using (FileStream fs = new FileStream(@"SampleData.txt", FileMode.Open, FileAccess.Read))
      {
        using (StreamReader reader = new StreamReader(fs))
        {
          while (!reader.EndOfStream)
          {
            string str = reader.ReadLine();
            if (str == "Data:")
            {
              // do nothing
            }
            else if (str == "Grid:")
            {
              // do nothing too
            }
            else
            {
              strings.Add(str);
            }
          }
        }
      }

      // data
      string[] nums = ParseDataString(strings[0]);
      int width = nums.Length;
      int height = strings.Count / 2;

      CultureInfo culture = new CultureInfo("ru-RU");

      double[,] data = new double[width, height];
      for (int row = 0; row < height; row++)
      {
        nums = ParseDataString(strings[row]);
        for (int column = 0; column < width; column++)
        {
          double d = double.Parse(nums[column], culture);
          data[column, row] = d;
        }
      }

      Point[,] gridData = new Point[width, height];
      for (int row = 0; row < height; row++)
      {
        string str = strings[row + height];
        nums = ParseGridString(str);
        for (int column = 0; column < width; column++)
        {
          string[] vecStrs = nums[column].Split(new string[] { "; " }, StringSplitOptions.None);
          gridData[column, row] = new Point(double.Parse(vecStrs[0], culture), double.Parse(vecStrs[1], culture));
        }
      }

      WarpedDataSource2D<double> dataSource = new WarpedDataSource2D<double>(data, gridData);
      isolineGraph.DataSource = dataSource;
      trackingGraph.DataSource = dataSource;

      //Rect visible = dataSource.GetGridBounds();
      //plotter.Viewport.Visible = visible;
    }

    private static string[] ParseDataString(string str)
    {
      str = str.TrimEnd(' ');
      return str.Split(' ');
    }

    private static string[] ParseGridString(string str)
    {
      return str.TrimEnd(' ').
        Substring(0, str.Length - 3).
        TrimStart('{').
        Split(new string[] { " } {" }, StringSplitOptions.None);
    }
  }
}
