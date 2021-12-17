using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SSJ2_Workout.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Settings : ContentPage
    {
        public Settings()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {

            base.OnAppearing();
            sounds.IsToggled = SavedData.sounds;
        }

        void OnToggled(object sender, ToggledEventArgs e)
        {
            if (!sounds.IsToggled)
                SavedData.sounds = false;
            else
                SavedData.sounds = true;
        }
    }
}