using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using static SSJ2_Workout.Views.Steps;
using Xamarin.Essentials;

namespace SSJ2_Workout.Views
{
    public partial class AboutPage : ContentPage, INotifyPropertyChanged
    {
       
        public AboutPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
            DependencyService.Get<IStepCounter>().InitSensorService();
            var dane = (MainViewModel)BindingContext;
            dane.Step = DependencyService.Get<IStepCounter>().Steps.ToString();

            Device.StartTimer(TimeSpan.FromMilliseconds(300), () => //dzialanie w tle tasku 
            {
                Task.Run(async () =>
                {
                    dane.Wzrost = Person.Wzrost.ToString();
                    dane.Wiek = Person.Wiek.ToString();
                    dane.Waga = Person.Waga.ToString();
                    dane.Bmi = Person.BMI.ToString();
                    dane.Bmr = Person.BMR.ToString();
                    dane.Step = DependencyService.Get<IStepCounter>().Steps.ToString();
                    dane.Suma = SavedData.sum_save;
                    dane.Suma2 = SavedData.sum2_save;
                    dane.Suma3 = SavedData.sum3_save;
                    dane.Suma4 = SavedData.sum4_save;
                    dane.Langi = SavedData.locat;
                    dane.Longi = SavedData.posit;
                    decimal x = Decimal.Divide(SavedData.sum2_save,SavedData.sum_save);
                    await zjedzone.ProgressTo((double)x, 1500, Easing.Linear);
                    decimal tmp = (decimal)dane.Sumarycznie;
                    decimal x2 = Decimal.Divide(tmp, Person.BMR);
                    await ogolne.ProgressTo((double)x2, 1500, Easing.Linear);
                });
                return true;
            });
            
        }

        public void GoToSteps(object obj, EventArgs args)
        {
            Navigation.PushAsync(new Steps());
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
            if(SavedData.data_set)
                Navigation.PushAsync(new Goals());
            else
                DependencyService.Get<IMessage>().ShortAlert("Najpierw uzupełnij parametry ciała!");
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