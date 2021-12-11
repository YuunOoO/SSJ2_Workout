using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.ComponentModel;
using static Xamarin.Essentials.Permissions;
using Android.App;

namespace SSJ2_Workout.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(false)]
    [Service]
    public partial class Steps : ContentPage, INotifyPropertyChanged
    {
        public interface IStepCounter
        {
            int Steps { get; set; }

            bool IsAvailable();

            void InitSensorService();

            void StopSensorService();
            void Zeruj();
        }

        public async Task GetSensorsAsync()             ///sprawdzanie uprawnien ale tego akurat nie potrzebujemy
        {
            /*var permissions = await Permissions.CheckStatusAsync<Permissions.Sensors>();
            if (permissions != PermissionStatus.Granted)
            {
                testowy1.Text = "Uprawnienia sa juz przyznane!";
                permissions = await Permissions.RequestAsync<Permissions.Sensors>();
            }
            testowy1.Text = "Przyznano uprawnienia!";*/
        }
        public async Task<PermissionStatus> CheckAndRequestPermissionAsync<T>(T permission)
                    where T : BasePermission
        {
            var status = await permission.CheckStatusAsync();
            if (status != PermissionStatus.Granted)
            {
                status = await permission.RequestAsync();
            }
            return status;
        }
        public Steps()
        {
            InitializeComponent();
            //GetSensorsAsync();
            BindingContext = new MainViewModel();
            var dane = (MainViewModel)BindingContext;
            //DependencyService.Get<IStepCounter>().InitSensorService();
            if (DependencyService.Get<IStepCounter>().IsAvailable())//jesli nie ma uprawnienen to je podajemy
            {
                DependencyService.Get<IStepCounter>().InitSensorService();
            }
            Accelerometer.ReadingChanged += Readchanged;

            Device.StartTimer(TimeSpan.FromMilliseconds(300), () => //dzialanie w tle tasku 
            {
                Task.Run(async () =>
                {
                    dane.Step = DependencyService.Get<IStepCounter>().Steps.ToString();
                });
                Device.BeginInvokeOnMainThread(() =>
                {
                    /*testowy.Text = "Twoje kroki to: " + dane.Step;*/
                });
                return true;
            });
        }


        private void Button_Clicked2(object sender, EventArgs e)
        {
            // nw jeszcze XDD
            DependencyService.Get<IStepCounter>().Zeruj();
        }

        void Readchanged(Object sender, AccelerometerChangedEventArgs args)
        {
            LabelX.Text = $"X: {args.Reading.Acceleration.X}";
            LabelY.Text = $"Y: {args.Reading.Acceleration.Y}";
            LabelZ.Text = $"Z: {args.Reading.Acceleration.Z}";
        }
        private async void NavigateButton_OnClicked(object sender, EventArgs e)
        {
            GetSensorsAsync();
            await Navigation.PushAsync(new AboutPage());
        }
        void ButtonClicked(Object Sender, EventArgs e)
        {
            if (Accelerometer.IsMonitoring)
            {
                Accelerometer.Stop();
                ButtonStart.Text = "Start";
            }
            else
            {
                Accelerometer.Start(SensorSpeed.UI);
                ButtonStart.Text = "Stop";
            }
        }
    }
}