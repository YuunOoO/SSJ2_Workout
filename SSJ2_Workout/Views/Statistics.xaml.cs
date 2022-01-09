using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microcharts;
using Syncfusion.SfChart.XForms;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace SSJ2_Workout.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Statistics : ContentPage
    {
       
        public Statistics()
        {
            InitializeComponent();
            //fastline.StrokeDashArray = new double[] { 2, 3 };
            MultiLine multiLine  = new MultiLine();

        }

        public class ViewModel
        {
            public ObservableCollection<ChartDataPoint> Data1 { get; set; }

            public ViewModel()
            {
                Data1 = new ObservableCollection<ChartDataPoint>();
                Data1.Add(new ChartDataPoint("Jan", 32));
                Data1.Add(new ChartDataPoint("Feb", 15));
                Data1.Add(new ChartDataPoint("Mar", 26));
                Data1.Add(new ChartDataPoint("Apr", 15));
                Data1.Add(new ChartDataPoint("May", 10));
                Data1.Add(new ChartDataPoint("Jun", 34));
                Data1.Add(new ChartDataPoint("Jul", 27));
                Data1.Add(new ChartDataPoint("Aug", 43));
                Data1.Add(new ChartDataPoint("Sep", 54));
                Data1.Add(new ChartDataPoint("Oct", 18));
                Data1.Add(new ChartDataPoint("Nov", 22));
                Data1.Add(new ChartDataPoint("Dec", 47));
            }

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            collectionView3.ItemsSource = await App<StoreData>.DatabaseStore.GetProductAsync();
        }     
    }
}