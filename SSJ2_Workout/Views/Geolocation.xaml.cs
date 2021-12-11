using Plugin.Geolocator;
using System;
using Xamarin.Essentials;
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
            var player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
            player.Load("gta.mp3");
            player.Play();
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;
            var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10000), null, true);
            SavedData.locat = position.Longitude.ToString();
            SavedData.posit = position.Latitude.ToString();
            LongitudeLabel.Text = SavedData.locat;
            LatitudeLabel.Text = SavedData.posit;
            Preferences.Set("LOCAT", $"{SavedData.locat}");
            Preferences.Set("POSIT", $"{SavedData.posit}");
        }
    }
}