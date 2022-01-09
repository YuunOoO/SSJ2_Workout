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

        double spalone;
        public double Spalone
        {
            set
            {
                if (spalone != value)
                {
                    spalone = value;
                    OnPropertyChanged(nameof(Spalone));
                }
            }
            get
            {
                return spalone;
            }
        }

        double sumarycznie;
        public double Sumarycznie
        {
            set
            {
                if (sumarycznie != value)
                {
                    sumarycznie = value;
                    SavedData.sumaryczniee = Convert.ToInt32(value);
                    OnPropertyChanged(nameof(Sumarycznie));
                }
            }
            get
            {
                return sumarycznie;
            }
        }
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

        int suma3;
        public int Suma3
        {
            set
            {
                if (suma3 != value)
                {
                    suma3 = value;
                    OnPropertyChanged(nameof(Suma3));
                }
            }
            get
            {
                return suma3;
            }
        }

        int suma4;
        public int Suma4
        {
            set
            {
                if (suma4 != value)
                {
                    
                    Spalone = Spalone - Suma4 + value;
                    if (Spalone < 0) Spalone = 0;
                    OnPropertyChanged(nameof(Spalone));
                    Sumarycznie = Oblicz_Kalorie();
                    OnPropertyChanged(nameof(Sumarycznie));
                    suma4 = value;
                    OnPropertyChanged(nameof(Suma4));
                }
            }
            get
            {
                return suma4;
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

        string waga, wzrost, bmi, bmr;
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
        public string Bmr
        {
            set
            {
                if (bmr != value)
                {
                    bmr = value;
                    OnPropertyChanged("Bmr");
                }
            }
            get
            {
                return bmr;
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
            if (SavedData.data_set)
            {
                int wa = Person.Waga;
                int wz = Person.Wzrost;
                decimal Calories_burned_per_km = Decimal.Divide((decimal)wa, (decimal)0.45359237) * (decimal)1.7; // calories per km     (ostatnia wartosc do editu)
                decimal Your_strip = Decimal.Multiply((decimal)wz, (decimal)0.415);
                decimal steps_in_1_km = Decimal.Divide((decimal)100000, Your_strip);
                decimal conversationFactor = Calories_burned_per_km / steps_in_1_km;

                double wynik = Convert.ToDouble(Convert.ToDecimal(kroki) * conversationFactor);
                wynik = Math.Round(wynik, 3);
                return wynik;
            }
            else
                return 0;
        }
        Double Oblicz_Kalorie()
        {
            return Suma2 - Spalone;
        }
    }
}
