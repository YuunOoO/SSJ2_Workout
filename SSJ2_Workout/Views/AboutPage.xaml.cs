using System;
using System.ComponentModel;
using Xamarin.Forms;
using static SSJ2_Workout.Views.Steps;

namespace SSJ2_Workout.Views
{
    public partial class AboutPage : ContentPage, INotifyPropertyChanged
    {
        public AboutPage()
        {
            InitializeComponent();
            BindingContext = new MyBindingObject();
           // podaj = DependencyService.Get<IStepCounter>().Steps.ToString();
            // MyStringProperty = $"Liczba krokow :  { mySteps }"; // It will be shown at your label
            //   wyswietlkroki.Text  = $"Liczba krokow:  { mySteps}";
        }
        public void GoToSteps(object obj, EventArgs args)
        {
            Navigation.PushAsync(new Steps());
        }
        public void GoToPulse(object obj, EventArgs args)
        {
            Navigation.PushAsync(new Pulse());
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
            Navigation.PushAsync(new Goals());
        }
        public void GoToSettings(object obj, EventArgs args)
        {
            Navigation.PushAsync(new Settings());
        }
    }

}