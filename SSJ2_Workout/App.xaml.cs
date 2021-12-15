using System;
using SSJ2_Workout.Services;
using SSJ2_Workout.Views;
using Xamarin.Forms;
using System.IO;
using static SSJ2_Workout.Views.Steps;
using Plugin.Geolocator.Abstractions;
using Xamarin.Essentials;

[assembly: ExportFont("Samantha.ttf")]

namespace SSJ2_Workout
{
    public partial class App<T> : Application where T : new()
    {

        private static Database<Product> database;
        private static Database<Exercise> databaseExercise;
        public static Database<Product> Database
        {
            get
            {
                if (database == null)
                {
                    database = new Database<Product>(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "product.db3"));
                }

                return database;
            }
        }

        public static Database<Exercise> DatabaseExercise
        {
            get
            {
                if (databaseExercise == null)
                {
                    databaseExercise = new Database<Exercise>(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "exercise.db3"));
                }

                return databaseExercise;
            }
        }

    }
    public partial class App : Application
    {
        IGeolocator locator = DependencyService.Get<IGeolocator>();
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
            if (Preferences.ContainsKey("WZROST"))
            {
                Person.Wzrost = Convert.ToInt32(Preferences.Get("WZROST", ""));
                Person.Wiek = Convert.ToInt32(Preferences.Get("WIEK", ""));
                Person.Waga = Convert.ToInt32(Preferences.Get("WAGA", ""));
                Person.BMI = Convert.ToDecimal(Preferences.Get("BMI", ""));
                Person.Gender = Preferences.Get("GENDER", "");
                Person.Mnoznik = Preferences.Get("MNOZNIK", "");
                SavedData.data_set = true;
            }
            if (Preferences.ContainsKey("LOCAT"))
            {
                SavedData.locat = Preferences.Get("LOCAT", "");
                SavedData.posit = Preferences.Get("POSIT", "");
            }
            if (Preferences.ContainsKey("SUM"))
            {
                SavedData.sum_save = Convert.ToInt32(Preferences.Get("SUM", ""));
                SavedData.sum2_save = Convert.ToInt32(Preferences.Get("SUM2", ""));
            }
            if (Preferences.ContainsKey("SUM3"))
            {
                SavedData.sum3_save = Convert.ToInt32(Preferences.Get("SUM3", ""));
                SavedData.sum4_save = Convert.ToInt32(Preferences.Get("SUM4", ""));
            }

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
