﻿using System;
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
        public double Spalone { get; set; }
        public double Sumarycznie { get; set; }
        public string Step
        {
            set
            {
                if (step != value)
                {
                    step = value;
                    Spalone = Oblicz_spalone(Step);
                    Sumarycznie = Oblicz_Kalorie();
                    OnPropertyChanged(nameof(Step));
                    OnPropertyChanged(nameof(Spalone));
                    OnPropertyChanged(nameof(Sumarycznie));
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
                    Sumarycznie = Oblicz_Kalorie();
                    OnPropertyChanged(nameof(Sumarycznie));
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
                if (waga != value)
                {
                    waga = value;
                    OnPropertyChanged("Waga");
                }
            }
            get
            {
                return waga;
            }
        }
        public string Wzrost
        {
            set
            {
                if (wzrost != value)
                {
                    wzrost = value;
                    OnPropertyChanged("Wzrost");
                }
            }
            get
            {
                return wzrost;
            }
        }

        public string Bmi
        {
            set
            {
                if (bmi != value)
                {
                    bmi = value;
                    OnPropertyChanged("Bmi");
                }
            }
            get
            {
                return bmi;
            }
        }

        public string Wiek
        {
            set
            {
                if (Preferences.Get(nameof(Wiek), "default_valu") != value)
                {
                    //wiek = value;
                    Preferences.Set(nameof(Wiek), value);
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

        double Oblicz_spalone(string kroki)
        {
            return (0.4 * (double)Person.BMI * Person.Wiek * 4);
        }
        Double Oblicz_Kalorie()
        {
            return Suma2 - Spalone;
        }
    }
}
