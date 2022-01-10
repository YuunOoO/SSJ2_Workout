using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SSJ2_Workout.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Calories : ContentPage
    {
        public Calories()
        {
            InitializeComponent();
        }

        public void GoToDelivered(object obj, EventArgs args)
        {
            Navigation.PushAsync(new CaloriesDeliverd());
        }
        public void GoToBurned(object obj, EventArgs args)
        {
            Navigation.PushAsync(new CaloriesBurned());
        }
        public void GoToMenu(object obj, EventArgs args)
        {
            Navigation.PushAsync(new PasekBoczny());
        }
    }
}