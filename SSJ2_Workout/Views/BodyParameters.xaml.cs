using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SSJ2_Workout.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BodyParameters : ContentPage
    {
        static string Name;
        public BodyParameters()
        {
            InitializeComponent();
            genderPicker.Items.Add("Kobieta");
            genderPicker.Items.Add("Mężczyzna");
            genderPicker.Items.Add("Inne");
            actionPicker.Items.Add("2.2 - osoby trenujące wyczynowo, profesjonalni sportowcy");
            actionPicker.Items.Add("2.0 - osoby o dużej aktywności fizycznej");
            actionPicker.Items.Add("1.7 - osoby o umiarkowanej aktywności fizycznej");
            actionPicker.Items.Add("1.4 - osoby o małej aktywności fizycznej");
            actionPicker.Items.Add("1.2 - osoby pozostające w bezruchu");
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

 
        async void BodyChange(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(wiekEntry.Text) && !string.IsNullOrWhiteSpace(wzrostEntry.Text) && !string.IsNullOrWhiteSpace(wagaEntry.Text))
            {
                if (IsDigitsOnly(wiekEntry.Text) && IsDigitsOnly(wzrostEntry.Text) && IsDigitsOnly(wagaEntry.Text))
                {
                    if (SavedData.sounds)
                    {
                        var player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
                        player.Load("goku.mp3");
                        player.Play();
                    }
                    Person.Waga = Convert.ToInt32(wagaEntry.Text);
                    Person.Wzrost = Convert.ToInt32(wzrostEntry.Text);
                    Person.Wiek = Convert.ToInt32(wiekEntry.Text);
                    Person.Gender = genderPicker.Items[genderPicker.SelectedIndex];
                    Person.BMI = Decimal.Divide(Person.Waga, (Decimal.Divide(Person.Wzrost, 100) * Decimal.Divide(Person.Wzrost, 100)));
                    Person.BMI = Convert.ToDecimal(Math.Round((double)Convert.ToDouble(Person.BMI), 3));
                    Person.Mnoznik = Convert.ToDecimal((actionPicker.Items[actionPicker.SelectedIndex]).Substring(0, 3));
                    SavedData.data_set = true;
                    Preferences.Set("MNOZNIK", Person.Mnoznik.ToString());
                    Preferences.Set("WAGA", $"{Person.Waga}");
                    Preferences.Set("WZROST", $"{Person.Wzrost}");
                    Preferences.Set("WIEK", $"{Person.Wiek}");
                    Preferences.Set("GENDER", $"{Person.Gender}");
                    Preferences.Set("BMI", Person.BMI.ToString());
                    wzrostEntry.Text = string.Empty;
                    wiekEntry.Text = string.Empty;
                    wagaEntry.Text = string.Empty;
                    if (!string.IsNullOrWhiteSpace(Person.Cel))
                    {
                        Goals goals = new Goals();
                        goals.KcalNeed2();
                    }
                    DependencyService.Get<IMessage>().ShortAlert("Pomyslnie zmieniono parametry!");
                }
                else
                {
                    DependencyService.Get<IMessage>().ShortAlert("Podano Niepoprawne dane ): ");
                }
            }
        }

        private void genderChoice(object sender, EventArgs e)
        {
            string name = genderPicker.Items[genderPicker.SelectedIndex];
            Name = name;
        }

        private void actionChoice(object sender, EventArgs e)
        {
            string name = actionPicker.Items[actionPicker.SelectedIndex];
            Name = name;
        }
    }
}