using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.ComponentModel;
using static Xamarin.Essentials.Permissions;
using Android.App;
using System.Timers;
using static SSJ2_Workout.Views.Steps;

namespace SSJ2_Workout.Views
{

    public class MainViewModel : INotifyPropertyChanged
    {
        string step;
        public string Step
        {
            set
            {
                if (step != value)
                {
                    step = value;
                    OnPropertyChanged(nameof(Step));
                }
            }
            get
            {
                return step;
            }
        }

        int suma;
        public int Suma
        {
            set
            {
                if (suma != value)
                {
                    suma = value;
                    OnPropertyChanged(nameof(Suma));
                }
            }
            get
            {
                return suma;
            }
        }

        int suma2;
        public int Suma2
        {
            set
            {
                if (suma2 != value)
                {
                    suma2 = value;
                    OnPropertyChanged(nameof(Suma2));
                }
            }
            get
            {
                return suma2;
            }
        }

        string longi, langi;

        public string Longi
        {
            set
            {
                if (longi != value)
                {
                    longi = value;
                    OnPropertyChanged("Longi");
                }
            }
            get
            {
                return longi;
            }
        }

        public string Langi
        {
            set
            {
                if (langi != value)
                {
                    langi = value;
                    OnPropertyChanged("Langi");
                }
            }
            get
            {
                return langi;
            }
        }

        string waga, wzrost, bmi, wiek;
        public string Waga
        {
            set
            {
                if (Preferences.Get(nameof(Waga), "default_value") != value)
                {
                    //wiek = value;
                    Preferences.Set(nameof(Waga), value);
                    OnPropertyChanged(nameof(Waga));
                }
            }
            get
            {
                return Preferences.Get(nameof(Waga), "default_value");
            }
        }
        public string Wzrost
        {
            set
            {
                if (Preferences.Get(nameof(Wzrost), "default_value") != value)
                {
                    //wiek = value;
                    Preferences.Set(nameof(Wzrost), value);
                    OnPropertyChanged(nameof(Wzrost));
                }
            }
            get
            {
                return Preferences.Get(nameof(Wzrost), "default_value");
            }
        }

        public string Bmi
        {
            set
            {
                if (Preferences.Get(nameof(Bmi), "default_value") != value)
                {
                    //wiek = value;
                    Preferences.Set(nameof(Bmi), value);
                    OnPropertyChanged(nameof(Bmi));
                }
            }
            get
            {
                return Preferences.Get(nameof(Bmi), "default_value");
            }
        }

        public string Wiek
        {
            set
            {
                if (Preferences.Get(nameof(Wiek), "default_value") != value)
                {
                    //wiek = value;
                    Preferences.Set(nameof(Wiek),value);
                    OnPropertyChanged(nameof(Wiek));
                }
            }
            get
            {
                return Preferences.Get(nameof(Wiek), "default_value");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
