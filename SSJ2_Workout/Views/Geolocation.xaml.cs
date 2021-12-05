using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SSJ2_Workout.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Geolocation : ContentPage
    {
        public Geolocation()
        {
            InitializeComponent();
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;
            var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10000), null, true);
            LongitudeLabel.Text = position.Longitude.ToString();
            LatitudeLabel.Text = position.Latitude.ToString();
        }
    }
}