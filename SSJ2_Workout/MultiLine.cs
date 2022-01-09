using System;
using System.Collections.Generic;
using System.Text;
using System;
using Xamarin.Forms;
using Syncfusion.SfChart.XForms;
using System.Collections.ObjectModel;


namespace SSJ2_Workout
{
    public class MultiLine : ContentPage
    {
        public MultiLine()
		{
			Title = "MultiLineSeries";
            SfChart chart;
			ObservableCollection<ChartDataPoint> Data1 = new ObservableCollection<ChartDataPoint>();
			ObservableCollection<ChartDataPoint> Data2 = new ObservableCollection<ChartDataPoint>();
			ObservableCollection<ChartDataPoint> Data3 = new ObservableCollection<ChartDataPoint>();
			ObservableCollection<ChartDataPoint> Data4 = new ObservableCollection<ChartDataPoint>();
			ObservableCollection<ChartDataPoint> Data5 = new ObservableCollection<ChartDataPoint>();
			ObservableCollection<ChartDataPoint> Data6 = new ObservableCollection<ChartDataPoint>();

			StackLayout layout = new StackLayout() { Padding = new Thickness(5, 15, 0, 5) };
			DateTime date = new DateTime(2002, 6, 16);
			Random rand = new Random();

			for (int i = 0; i < 30; i++)
			{
				Data1.Add(new ChartDataPoint(date.AddDays(i), rand.Next(40, 50)));
				Data2.Add(new ChartDataPoint(date.AddDays(i), rand.Next(45, 55)));
				Data3.Add(new ChartDataPoint(date.AddDays(i), rand.Next(150, 175)));
				Data4.Add(new ChartDataPoint(date.AddDays(i), rand.Next(175, 200)));
				Data5.Add(new ChartDataPoint(date.AddDays(i), rand.Next(285, 300)));
				Data6.Add(new ChartDataPoint(date.AddDays(i), rand.Next(360, 440)));
			}

			chart = new SfChart();

			DateTimeAxis dataxis = new DateTimeAxis()
			{
				Interval = 1,
				IntervalType = DateTimeIntervalType.Days,
				LabelStyle = new ChartAxisLabelStyle() { LabelFormat = "dd" }
			};
			chart.PrimaryAxis = dataxis;

			NumericalAxis numaxis = new NumericalAxis();
			chart.SecondaryAxis = numaxis;

			var line1 = new LineSeries()
			{
				ItemsSource = Data3,
				Label = "Published",
				Color = Color.Purple,
				DataMarker = new ChartDataMarker()
				{
					ShowLabel = false,
					ShowMarker = true,
					MarkerHeight = 5,
					MarkerWidth = 5,
					MarkerColor = Color.Purple
				}
			};

			var line2 = new LineSeries()
			{
				ItemsSource = Data2,
				Label = "Accepted",
				Color = Color.Aqua,
				DataMarker = new ChartDataMarker()
				{
					ShowLabel = false,
					ShowMarker = true,
					MarkerHeight = 5,
					MarkerWidth = 5,
					MarkerColor = Color.Aqua
				}
			};

			var line3 = new LineSeries()
			{
				ItemsSource = Data1,
				Label = "Active",
				Color = Color.Yellow,
				DataMarker = new ChartDataMarker()
				{
					ShowLabel = false,
					ShowMarker = true,
					MarkerHeight = 5,
					MarkerWidth = 5,
					MarkerColor = Color.Yellow
				}
			};

			var line4 = new LineSeries()
			{
				ItemsSource = Data6,
				Label = "Completed",
				Color = Color.Olive,
				DataMarker = new ChartDataMarker()
				{
					ShowLabel = false,
					ShowMarker = true,
					MarkerHeight = 5,
					MarkerWidth = 5,
					MarkerColor = Color.Olive
				}
			};

			var line5 = new LineSeries()
			{
				ItemsSource = Data5,
				Label = "Cancelled",
				Color = Color.Red,
				DataMarker = new ChartDataMarker()
				{
					ShowLabel = false,
					ShowMarker = true,
					MarkerHeight = 5,
					MarkerWidth = 5,
					MarkerColor = Color.Red
				}
			};

			var line6 = new LineSeries()
			{
				ItemsSource = Data4,
				Label = "Expired",
				Color = Color.Pink,
				DataMarker = new ChartDataMarker()
				{
					ShowLabel = false,
					ShowMarker = true,
					MarkerHeight = 5,
					MarkerWidth = 5,
					MarkerColor = Color.Pink
				}
			};

			chart.VerticalOptions = LayoutOptions.FillAndExpand;
			chart.Legend = new ChartLegend();
			chart.Legend.DockPosition = LegendPlacement.Bottom;

			chart.Series.Add(line1);
			chart.Series.Add(line2);
			chart.Series.Add(line3);
			chart.Series.Add(line4);
			chart.Series.Add(line5);
			chart.Series.Add(line6);
			layout.Children.Add(chart);
			Content = layout;

		}
	}
}
