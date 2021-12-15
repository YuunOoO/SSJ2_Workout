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
    public partial class Goals : ContentPage
    {
        static string Name;
        public Goals()
        {
            InitializeComponent();
            genderPicker.Items.Add("Kobieta");
            genderPicker.Items.Add("Mężczyzna");
            genderPicker.Items.Add("Inne");
            goalPicker.Items.Add("Szybkie schudnięcie");
            goalPicker.Items.Add("Schudnięcie");
            goalPicker.Items.Add("Utrzymanie wagi");
            goalPicker.Items.Add("Przybranie na wadze");
            goalPicker.Items.Add("Szybkie przybranie na wadze");


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
        async void KcalNeed(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(wiekEntry.Text) && !string.IsNullOrWhiteSpace(wzrostEntry.Text) && !string.IsNullOrWhiteSpace(wagaEntry.Text))
            {
                if (IsDigitsOnly(wiekEntry.Text) && IsDigitsOnly(wzrostEntry.Text) && IsDigitsOnly(wagaEntry.Text))
                {
                    //var player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
                    //player.Load("goku.mp3");
                    //player.Play();
                    Person.Waga = Convert.ToInt32(wagaEntry.Text);
                    Person.Wzrost = Convert.ToInt32(wzrostEntry.Text);
                    Person.Wiek = Convert.ToInt32(wiekEntry.Text);
                    Person.Gender = genderPicker.Items[genderPicker.SelectedIndex];
                    Person.Cel = goalPicker.Items[goalPicker.SelectedIndex];
                    if (Person.Gender =="Mężczyzna")
                    {
                        if( Person.Cel == "Szybkie schudnięcie")
                        {
                            Person.BMR = (((Person.Waga * 10) + (Person.Wzrost * 7) - (Person.Wiek * 5)) + 5) - 400;
                        }
                        else if (Person.Cel == "Schudnięcie")
                        {
                            Person.BMR = (((Person.Waga * 10) + (Person.Wzrost * 7) - (Person.Wiek * 5)) + 5) - 200;
                        }
                        else if (Person.Cel == "Utrzymanie wagi")
                        {
                            Person.BMR = (((Person.Waga * 10) + (Person.Wzrost * 7) - (Person.Wiek * 5)) + 5);
                        }
                        else if (Person.Cel == "Przybranie na wadze")
                        {
                            Person.BMR = (((Person.Waga * 10) + (Person.Wzrost * 7) - (Person.Wiek * 5)) + 5) + 200;
                        }
                        else if (Person.Cel == "Szybkie przybranie na wadze")
                        {
                            Person.BMR = (((Person.Waga * 10) + (Person.Wzrost * 7) - (Person.Wiek * 5)) + 5) + 400;
                        }

                    }
                    else
                    {
                        if (Person.Cel == "Szybkie schudnięcie")
                        {
                            Person.BMR = (((Person.Waga * 10) + (Person.Wzrost * 7) - (Person.Wiek * 5)) - 162) - 300;
                        }
                        else if (Person.Cel == "Schudnięcie")
                        {
                            Person.BMR = (((Person.Waga * 10) + (Person.Wzrost * 7) - (Person.Wiek * 5)) - 162) - 150;
                        }
                        else if (Person.Cel == "Utrzymanie wagi")
                        {
                            Person.BMR = (((Person.Waga * 10) + (Person.Wzrost * 7) - (Person.Wiek * 5)) - 162);
                        }
                        else if (Person.Cel == "Przybranie na wadze")
                        {
                            Person.BMR = (((Person.Waga * 10) + (Person.Wzrost * 7) - (Person.Wiek * 5)) - 162) + 150;
                        }
                        else if (Person.Cel == "Szybkie przybranie na wadze")
                        {
                            Person.BMR = (((Person.Waga * 10) + (Person.Wzrost * 7) - (Person.Wiek * 5)) - 162) + 300;
                        }
                    }

                    //Person.BMR = Decimal.Divide(Person.Waga, (Decimal.Divide(Person.Wzrost, 100) * Decimal.Divide(Person.Wzrost, 100)));
                    //Person.BMR = Convert.ToDecimal(Math.Round((double)Convert.ToDouble(Person.BMI), 3));
                    Person.Mnoznik = goalPicker.Items[goalPicker.SelectedIndex];
                    SavedData.data_set = true;
                    Preferences.Set("BMR", Person.BMR.ToString());
                    wzrostEntry.Text = string.Empty;
                    wiekEntry.Text = string.Empty;
                    wagaEntry.Text = string.Empty;
                    DependencyService.Get<IMessage>().ShortAlert("Pomyslnie obliczono zapotrzebowanie!");
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
        private void goalChoice(object sender, EventArgs e)
        {
            string name = goalPicker.Items[goalPicker.SelectedIndex];
            Name = name;
        }
    }
}