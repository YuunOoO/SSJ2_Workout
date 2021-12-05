using System;
using System.Threading.Tasks;
using SSJ2_Workout.Services;
using SSJ2_Workout.Views;
using Xamarin.Forms;
using System.IO;
using static SSJ2_Workout.Views.Steps;
using static SSJ2_Workout.Views.MainViewModel;
using Plugin.Geolocator.Abstractions;

[assembly: ExportFont("Samantha.ttf")]

namespace SSJ2_Workout
{
    public partial class App : Application
    {
        IGeolocator locator = DependencyService.Get<IGeolocator>();

        private static Database database;

        public static Database Database
        {
            get
            {
                if (database == null)
                {
                    database = new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "product.db3"));
                }

                return database;
            }
        }
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
            DependencyService.Get<IStepCounter>().InitSensorService();
        }

        protected override void OnResume()
        {
            DependencyService.Get<IStepCounter>().InitSensorService();
        }


    }
}
