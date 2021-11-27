using System;
using SSJ2_Workout.Services;
using SSJ2_Workout.Views;
using Xamarin.Forms;
using Plugin;
using Xamarin.Essentials;
using Android.OS;

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

        //protected override void OnCreate(Bundle savedInstanceState)
        //{
        //    //...
        //    base.OnCreate(savedInstanceState);
        //    Xamarin.Essentials.Platform.Init(this, savedInstanceState); // add this line to your code, it may also be called: bundle
        //                                                                //...
        //}




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
