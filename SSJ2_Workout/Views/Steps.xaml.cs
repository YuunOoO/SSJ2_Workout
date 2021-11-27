using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Plugin.Sensors;
using SQLitePCL;
using SQLite;
using Plugin;
using System.ComponentModel;
using static Xamarin.Essentials.Permissions;
using System.Windows.Input;

namespace SSJ2_Workout.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(false)]
    public partial class Steps : ContentPage
    {
        public interface IStepCounter
        {
            int Steps { get; set; }

            bool IsAvailable();

            void InitSensorService();

            void StopSensorService();
        }
        public async Task GetSensorsAsync()
        {
            var permissions = await Permissions.CheckStatusAsync<Permissions.Sensors>();
            if (permissions != PermissionStatus.Granted)
            {
                testowy.Text = "Uprawnienia sa juz przyznane!";
                permissions = await Permissions.RequestAsync<Permissions.Sensors>();
            }
            
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
            DependencyService.Get<IStepCounter>().InitSensorService();
            myBtn.IsVisible = true;
            if (DependencyService.Get<IStepCounter>().IsAvailable())
            {
                myBtn.IsVisible = true;
                DependencyService.Get<IStepCounter>().InitSensorService();
            }
            Accelerometer.ReadingChanged += Readchanged;
        }

        private void Button_Clicked2(object sender, EventArgs e)
        {
            mylabel.Text =$"Liczba krokow:  { DependencyService.Get<IStepCounter>().Steps.ToString()}";
        }

        /*  public class PedometerImpl : AbstractSensor<int>, IPedometer
          {
              public PedometerImpl() : base(SensorType.StepCounter) { }
              protected override int ToReading(SensorEvent e) => Convert.ToInt32(e.Values[0]);
          }
        */


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
            // Device.BeginInvokeOnMainThread;
        }

    }
}