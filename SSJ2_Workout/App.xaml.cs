using System;
using System.Threading.Tasks;
using SSJ2_Workout.Services;
using SSJ2_Workout.Views;
using Xamarin.Forms;
using static SSJ2_Workout.Views.Steps;

[assembly: ExportFont("Samantha.ttf")]

namespace SSJ2_Workout
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            DependencyService.Get<IStepCounter>().InitSensorService();
            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
            MainPage = new NavigationPage(new AboutPage());
        }
        protected override void OnStart()
        {
            DependencyService.Get<IStepCounter>().InitSensorService();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }


    }
}
