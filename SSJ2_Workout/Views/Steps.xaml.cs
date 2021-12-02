using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.ComponentModel;
using static Xamarin.Essentials.Permissions;
using Android.App;
using System.Timers;
namespace SSJ2_Workout.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(false)]
    [Service]
    public partial class Steps : ContentPage
    {
        public interface IStepCounter
        {
            int Steps { get; set; }

            bool IsAvailable();

            void InitSensorService();

            void StopSensorService();
        }

        public static string mySteps = "0";
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
           // BindingContext = new MySteps();
            myBtn.IsVisible = true;
            DependencyService.Get<IStepCounter>().InitSensorService();
            if (DependencyService.Get<IStepCounter>().IsAvailable())
            {
                myBtn.IsVisible = true;
                DependencyService.Get<IStepCounter>().InitSensorService();
            }
            Accelerometer.ReadingChanged += Readchanged;
            
             System.Timers.Timer t = new System.Timers.Timer(1000);
             t.Elapsed += new System.Timers.ElapsedEventHandler(getDate);
             t.AutoReset = true;
             t.Enabled = true;

            
            

            void getDate(object sender, ElapsedEventArgs e)
            {
                mySteps = DependencyService.Get<IStepCounter>().Steps.ToString();
                kroczki.Text = $"Przebyte kroki : {mySteps}";
            }
        }

        private void Button_Clicked2(object sender, EventArgs e)
        {
            mySteps = DependencyService.Get<IStepCounter>().Steps.ToString();
            //mylabel.Text = $"Liczba krokow:  { mySteps }";
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