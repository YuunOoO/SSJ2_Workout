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
            goalPicker.Items.Add("Szybkie schudnięcie");
            goalPicker.Items.Add("Schudnięcie");
            goalPicker.Items.Add("Utrzymanie wagi");
            goalPicker.Items.Add("Przybranie na wadze");
            goalPicker.Items.Add("Szybkie przybranie na wadze");
            goalPicker.Items.Add("**Wybierz ręcznie własny plan");

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

        public void KcalNeed2()
        {
            if (Person.Gender == "Mężczyzna")
            {
                if (Person.Cel == "Szybkie schudnięcie")
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
            Person.BMR *= Person.Mnoznik;
            Preferences.Set("BMR", Person.BMR.ToString());
        }
        async void KcalNeed(object sender, EventArgs e)
        {
            Person.Cel = goalPicker.Items[goalPicker.SelectedIndex];
            KcalNeed2();
            Noti3.Text = $"{Person.BMR}";
            Noti.Text = "Zatwierdz lub zmien swoj cel";
            DependencyService.Get<IMessage>().ShortAlert("Pomyslnie obliczono zapotrzebowanie!");
        }
    
        private void goalChoice(object sender, EventArgs e)
        {
            string name = goalPicker.Items[goalPicker.SelectedIndex];
            Name = name;
        }
    }
}