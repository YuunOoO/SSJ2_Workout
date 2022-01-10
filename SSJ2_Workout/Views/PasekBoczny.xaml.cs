using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SSJ2_Workout.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PasekBoczny : ContentPage
    {
        public PasekBoczny()
        {
            InitializeComponent();
        }
        public void GoToSteps(object obj, EventArgs args)
        {
            Navigation.PushAsync(new Steps());
        }
        public void GoToAbout(object obj, EventArgs args)
        {
            Navigation.PushAsync(new AboutPage());
        }
        public void GoToGeolocation(object obj, EventArgs args)
        {
            Navigation.PushAsync(new Geolocation());
        }
        public void GoToBMI(object obj, EventArgs args)
        {
            Navigation.PushAsync(new BodyParameters());
        }
        public void GoToCalories(object obj, EventArgs args)
        {
            Navigation.PushAsync(new Calories());
        }
        public void GoToMenu(object obj, EventArgs args)
        {
            Navigation.PushAsync(new PasekBoczny());
        }
        public void GoToNotifications(object obj, EventArgs args)
        {
            Navigation.PushAsync(new Notifications());
        }
        public void GoToGoals(object obj, EventArgs args)
        {
            if (SavedData.data_set)
                Navigation.PushAsync(new Goals());
            else
                DependencyService.Get<IMessage>().ShortAlert("Najpierw uzupełnij parametry ciała!");
        }
        public void GoToStat(object obj, EventArgs args)
        {
            Navigation.PushAsync(new Statistics());
        }
        public void GoToSettings(object obj, EventArgs args)
        {
            Navigation.PushAsync(new Settings());
        }
        public void GoToPulse(object obj, EventArgs args)
        {
            Navigation.PushAsync(new Pulse());
        }
        public void GoToStoper(object obj, EventArgs args)
        {
            Navigation.PushAsync(new Stoper());
        }

    }
}