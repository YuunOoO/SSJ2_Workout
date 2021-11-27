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
        public Steps()
        {
            InitializeComponent();
            Accelerometer.ReadingChanged += Readchanged;
           if (DependencyService.Get<IStepCounter>().IsAvailable())
           {
                DependencyService.Get<IStepCounter>().InitSensorService();
                myBtn.IsVisible = true;
           }
        }
        private void Button_Clicked2(object sender, EventArgs e)
        {
            mylabel.Text = DependencyService.Get<IStepCounter>().Steps.ToString();
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
            await Navigation.PushAsync(new AboutPage());
        }
        void ButtonClicked(Object Sender, EventArgs e)
        {
            if(Accelerometer.IsMonitoring)
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