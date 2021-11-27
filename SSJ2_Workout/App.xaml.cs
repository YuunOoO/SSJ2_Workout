using System;
using SSJ2_Workout.Services;
using SSJ2_Workout.Views;
using Xamarin.Forms;
using Plugin;

[assembly: ExportFont("Samantha.ttf")]

namespace SSJ2_Workout
{
    public partial class App : Application
    {
      
        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
            MainPage = new NavigationPage(new AboutPage());
       
        }

      


        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
