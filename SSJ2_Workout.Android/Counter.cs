using System;
using Android.App;
using Android.Content;
using Android.Hardware;
using Android.Runtime;
using static SSJ2_Workout.Views.Steps;

namespace SSJ2_Workout.Droid
{
    [Activity(Label = "Counter")]
    [assembly: Dependency(typeof(Counter))]

    public class Counter : Java.Lang.Object, IStepCounter, ISensorEventListener
    {

        public int StepsCounter = 0;
        private SensorManager sManager;
        public int Steps
        {
            get { return StepsCounter; }
            set { StepsCounter = value; }
        }
        public void Dispose()
        {
            sManager.UnregisterListener(this);
            sManager.Dispose();
        }
        public void InitSensorService()
        {
            sManager = Application.Context.GetSystemService(Context.SensorService) as SensorManager;
            sManager.RegisterListener(this, sManager.GetDefaultSensor(SensorType.StepCounter), SensorDelay.Normal);
        }
        public void OnAccuracyChanged(Sensor sensor, [GeneratedEnum] SensorStatus accuracy)
        {


            Console.WriteLine("OnAccuracyChanged called");
        }
        public void OnSensorChanged(SensorEvent e)
        {
            StepsCounter++;
            Console.WriteLine(e.ToString());
        }
        public void StopSensorService()
        {
            sManager.UnregisterListener(this);
        }
        public bool IsAvailable()
        {
            return Application.Context.PackageManager.HasSystemFeature(Android.Content.PM.PackageManager.FeatureSensorStepCounter) &&
                Application.Context.PackageManager.HasSystemFeature(Android.Content.PM.PackageManager.FeatureSensorStepDetector);
        }
     }
}