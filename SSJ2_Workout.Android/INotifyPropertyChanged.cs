using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SSJ2_Workout.Views;
using Xamarin.Forms;
using static SSJ2_Workout.Views.Steps;
namespace SSJ2_Workout.Droid
{
    [Activity(Label = "INotifyPropertyChanged")]
    public class INotifyPropertyChanged : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            System.Timers.Timer t = new System.Timers.Timer(500);
            t.Elapsed += new System.Timers.ElapsedEventHandler(getDate);
            t.AutoReset = true;
            t.Enabled = true;

            static void getDate(object sender, ElapsedEventArgs e)
            {
                mySteps = ((uint)DependencyService.Get<IStepCounter>().Steps);
                //updat();
            }
        }
    }
}