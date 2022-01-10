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
        public void GoToMenu(object obj, EventArgs args)
        {
            Navigation.PushAsync(new PasekBoczny());
        }
        private async void Button_OnClicked(object sender, EventArgs e)
        {
            if(SavedData.sounds)
            {
                var player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
                player.Load("gta.mp3");
                player.Play();
            }
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;
            var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10000), null, true);
            SavedData.locat = position.Longitude.ToString();
            SavedData.posit = position.Latitude.ToString();
            SavedData.locat = (Math.Round((double)Convert.ToDouble(SavedData.locat),4)).ToString();//o chuj ale combo zrobilem xDDDD
            SavedData.posit = (Math.Round((double)Convert.ToDouble(SavedData.posit), 4)).ToString();
            LongitudeLabel.Text = SavedData.locat;
            LatitudeLabel.Text = SavedData.posit;
            Preferences.Set("LOCAT", $"{SavedData.locat}");
            Preferences.Set("POSIT", $"{SavedData.posit}");
        }
    }
}