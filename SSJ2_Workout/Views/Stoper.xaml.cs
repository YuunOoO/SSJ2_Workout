using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SSJ2_Workout.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Stoper : ContentPage, INotifyPropertyChanged
    {
        int Czas;
        bool dodawaj = true;
        Timer timer;
        int hours = 0, mins = 0, secs = 0, milliseconds = 0;

        public Stoper()
        {
            InitializeComponent();
            BindingContext = this;
            Title = "Stoper";
        }

        private void BtnStart_Clicked(object sender, EventArgs e)
        {
          timer = new Timer();
        timer.Interval = 1; // 1 milliseconds  
                timer.Elapsed += Timer_Elapsed; 
            //timer.AutoReset = true;
            timer.Enabled = true;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (dodawaj)
            {
                milliseconds++;
                if (milliseconds >= 1000)
                {
                    secs++;
                    milliseconds = 0;
                }
                if (secs == 59)
                {
                    mins++;
                    secs = 0;

                }
                if (mins == 59)
                {
                    hours++;
                    mins = 0;
                }
            }
            else
            {
                milliseconds--;
                if (hours == 0)
                {
                    if (mins == 0)
                    {
                        if (secs <= 0)
                        {
                            if (milliseconds <= 0)
                            {
                                timer.Stop();
                                timer.Close();
                                dodawaj = true;
                                DependencyService.Get<IMessage>().ShortAlert("Stop!");
                                /// miejsce na kod alarmu
                            }
                        }
                    }
                }

                if (mins <= 0 && secs <= 0 && milliseconds <= 0)
                {
                    hours--;
                    mins = 59;
                    secs = 59;
                    milliseconds = 1000;
                }
                else if (secs <= 0 && milliseconds <= 0)
                {
                    mins--;
                    secs = 59;
                    milliseconds = 1000;
                }
                else if (milliseconds <= 0)
                {
                    secs--;
                    milliseconds = 1000;
                }
            }

            Device.BeginInvokeOnMainThread(() =>
            {
                lbl_result.Text = string.Format("{0:00}:{1:00}:{2:00}.{3:00}", hours, mins, secs, milliseconds / 10);
            });
        }


        private void BtnStop_Clicked(object sender, EventArgs e)
        {
            timer.Stop();
            //timer = null;
            dodawaj = true;
        }

        private void BtnReset_Clicked(object sender, EventArgs e)
        {
            timer.Close();
            hours = mins = secs = milliseconds = 0;
        }

        private void timeBtn_Clicked(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(timeEntry.Text))
            {
                if (IsDigitsOnly(timeEntry.Text))
                {
                    Czas = Convert.ToInt32(timeEntry.Text);
                    topek.Text = $"Wybrałes {Czas} do odmierzenia!";

                    hours = mins = secs = 0;

                    while (Czas >= 3600)
                    {
                        hours++;
                        Czas -= 3600;
                    }
                    while (Czas >= 60)
                    {
                        mins++;
                        Czas -= 60;
                    }
                    secs = Czas;
                    dodawaj = false;

                    lbl_result.Text = string.Format("{0:00}:{1:00}:{2:00}.{3:00}", hours, mins, secs, milliseconds / 10);

                    timeEntry.Text = string.Empty;



                }
                else
                    DependencyService.Get<IMessage>().ShortAlert("Podales zle dane ): ");
            }
           
        }

        bool IsDigitsOnly(string str) //chroni przed podaniem liter
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
    }
}