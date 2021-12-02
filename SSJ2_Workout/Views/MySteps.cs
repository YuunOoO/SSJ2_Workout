using System.ComponentModel;
namespace SSJ2_Workout.Views
{
    public class MySteps : INotifyPropertyChanged
    {
        string step;
        public string Step
        {
            set
            {
                if (step != value)
                {
                    step = value;
                    OnPropertyChanged("Step");
                   // OnPropertyChanged(nameof(DisplayStep));
                }
            }
            get
            {
                return step;
            }
        }
        //public string DisplayStep => $"Twoja liczba krokow to: {Step}";

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
