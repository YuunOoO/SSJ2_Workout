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


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
