using System;
using SSJ2_Workout.Services;
using SSJ2_Workout.Views;
using Xamarin.Forms;
using System.IO;
using static SSJ2_Workout.Views.Steps;
using Plugin.Geolocator.Abstractions;
using Xamarin.Essentials;
using System.Threading.Tasks;

[assembly: ExportFont("Samantha.ttf")]

namespace SSJ2_Workout
{
    public partial class App<T> : Application where T : new()
    {
        private static Database<Product> database;
        private static Database<Exercise> databaseExercise;
        private static Database<StoreData> databaseStore;

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

        public static Database<StoreData> DatabaseStore
        {
            get
            {
                if (databaseStore == null)
                {
                    databaseStore = new Database<StoreData>(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "store.db3"));
                }
                return databaseStore;
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
                Person.Mnoznik = Convert.ToDecimal(Preferences.Get("MNOZNIK", ""));
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
            if(Preferences.ContainsKey("BMR"))
            {
                Person.BMR = Convert.ToDecimal(Preferences.Get("BMR", ""));
            }
            if (Preferences.ContainsKey("data"))
            {

                SavedData.data_save = Preferences.Get("data", "");
            }
            else
            {
                string tmp = DateTime.Now.ToString();
                string tmp2 = string.Empty;
                for (int i = 0; i < tmp.Length; i++)
                {
                    if (tmp[i] != ' ')
                        tmp2 += tmp[i];
                    else
                        break;
                }


                SavedData.data_save = tmp2;
                Preferences.Set("data", SavedData.data_save);
            }


            bool reset = false;
            Device.StartTimer(TimeSpan.FromMinutes(1), () =>
            {

                Task.Run(async () =>
                {
                    string tmp = DateTime.Now.ToString();
                    string tmp2 = string.Empty;
                    for (int i = 0; i < tmp.Length; i++)
                    {
                        if (tmp[i] != ' ')
                            tmp2 += tmp[i];
                        else
                            break;
                    }
                    if (tmp2 != SavedData.data_save)    //zmienila sie data
                    {
                        SavedData.data_save = tmp2;
                        Preferences.Set("data", SavedData.data_save);
                        reset = true;

                        await App<StoreData>.DatabaseStore.SaveProductAsync(new StoreData
                        {
                          Day = SavedData.data_save,
                          Total_burned = SavedData.spalone,
                          Total_calories = SavedData.sumaryczniee,
                          Total_delivered = SavedData.sum2_save,
                          Total_steps = SavedData.kroki,
                        });        
                       

                    }
                });
                if(reset)
                {
                    DependencyService.Get<IMessage>().ShortAlert("Zapisano statystki!");

                    DependencyService.Get<IStepCounter>().Zeruj();
                    CaloriesBurned.zeruj();
                    CaloriesDeliverd.zeruj();
                    SavedData.spalone = SavedData.sum2_save = SavedData.sum3_save = SavedData.sum4_save = SavedData.sumaryczniee = SavedData.sum_save = 0;
                    reset = false;
                }
                

                return true;
            });

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
