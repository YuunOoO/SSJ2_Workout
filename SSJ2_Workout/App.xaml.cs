﻿using System;
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
            Person.Wzrost = Convert.ToInt32(Preferences.Get("WZROST", ""));
            Person.Wiek = Convert.ToInt32(Preferences.Get("WIEK", ""));
            Person.Waga = Convert.ToInt32(Preferences.Get("WAGA", ""));
            Person.BMI = Convert.ToDecimal(Preferences.Get("BMI", ""));
            SavedData.locat = Preferences.Get("LOCAT", "");
            SavedData.posit = Preferences.Get("POSIT", "");
            SavedData.sum_save = Convert.ToInt32(Preferences.Get("SUM", ""));
            SavedData.sum2_save = Convert.ToInt32(Preferences.Get("SUM2", ""));
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
