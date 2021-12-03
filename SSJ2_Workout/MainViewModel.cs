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
         //           OnPropertyChanged(nameof(DisplayStep));
                }
            }
            get
            {
                return step;
            }
        }
       // static public string DisplayStep => $"Twoja liczba krokow to:{Step}";

        
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
