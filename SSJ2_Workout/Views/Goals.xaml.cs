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
            goalPicker.Items.Add("Wybierz ręcznie własny plan");

        }

        void OnSliderValueChanged(object sender, ValueChangedEventArgs args)
        {
            double spalone = args.NewValue;
            double dostarczone = 100 - spalone;
            displayLabel.Text = $"Spalone {spalone}%  - Dostarczone{dostarczone}% ";
            pomoc.Text = "Preferowany stosunek to około ~25%(?) ";
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
            Person.WlasnyCel = Convert.ToInt32(WlasneKcalEntry.Text);
            if (Person.Cel == "Wybierz ręcznie własny plan")
            {
                Person.BMR = Person.WlasnyCel;
            }
            else
            {
                if (Person.Gender == "Mężczyzna")
                {
                    if (Person.Cel == "Szybkie schudnięcie")
                    {
                        Person.BMR = (((Person.Waga * 10) + (Person.Wzrost * 6) - (Person.Wiek * 5)) + 5) - 400;
                    }
                    else if (Person.Cel == "Schudnięcie")
                    {
                        Person.BMR = (((Person.Waga * 10) + (Person.Wzrost * 6) - (Person.Wiek * 5)) + 5) - 200;
                    }
                    else if (Person.Cel == "Utrzymanie wagi")
                    {
                        Person.BMR = (((Person.Waga * 10) + (Person.Wzrost * 6) - (Person.Wiek * 5)) + 5);
                    }
                    else if (Person.Cel == "Przybranie na wadze")
                    {
                        Person.BMR = (((Person.Waga * 10) + (Person.Wzrost * 6) - (Person.Wiek * 5)) + 5) + 200;
                    }
                    else if (Person.Cel == "Szybkie przybranie na wadze")
                    {
                        Person.BMR = (((Person.Waga * 10) + (Person.Wzrost * 6) - (Person.Wiek * 5)) + 5) + 400;
                    }
                }
                else
                {
                    if (Person.Cel == "Szybkie schudnięcie")
                    {
                        Person.BMR = (((Person.Waga * 10) + (Person.Wzrost * 6) - (Person.Wiek * 5)) - 162) - 300;
                    }
                    else if (Person.Cel == "Schudnięcie")
                    {
                        Person.BMR = (((Person.Waga * 10) + (Person.Wzrost * 6) - (Person.Wiek * 5)) - 162) - 150;
                    }
                    else if (Person.Cel == "Utrzymanie wagi")
                    {
                        Person.BMR = (((Person.Waga * 10) + (Person.Wzrost * 6) - (Person.Wiek * 5)) - 162);
                    }
                    else if (Person.Cel == "Przybranie na wadze")
                    {
                        Person.BMR = (((Person.Waga * 10) + (Person.Wzrost * 6) - (Person.Wiek * 5)) - 162) + 150;
                    }
                    else if (Person.Cel == "Szybkie przybranie na wadze")
                    {
                        Person.BMR = (((Person.Waga * 10) + (Person.Wzrost * 6) - (Person.Wiek * 5)) - 162) + 300;
                    }
                }
                Person.BMR *= Person.Mnoznik;
                Preferences.Set("BMR", Person.BMR.ToString());
            }
           
        }
        async void KcalNeed(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(WlasneKcalEntry.Text))
            {
                Person.Cel = goalPicker.Items[goalPicker.SelectedIndex];
                KcalNeed2();
                Noti3.Text = $"{Person.BMR}";
                DependencyService.Get<IMessage>().ShortAlert("Pomyslnie obliczono zapotrzebowanie!");
            }
            else
            {
                if (IsDigitsOnly(WlasneKcalEntry.Text))
                {
                    Person.BMR = Convert.ToDecimal((WlasneKcalEntry.Text));
                    Noti3.Text = WlasneKcalEntry.Text;
                }
                else
                    return;
            
            }
            Noti.Text = "Zatwierdz lub zmien swoj cel";
            slider.IsVisible = true;
            pomoc.IsVisible = true;
            displayLabel.IsVisible = true;
            btnZatwierdz.IsVisible = true;
          
        }
    
        private void goalChoice(object sender, EventArgs e)
        {
            string name = goalPicker.Items[goalPicker.SelectedIndex];
            Name = name;
        }

        private void btnZatwierdz_Clicked(object sender, EventArgs e)
        {
            Person.Stosunek = Convert.ToDecimal(slider.Value); //spalone %
            Person.Spalone_cel = Decimal.Divide(Person.BMR * Person.Stosunek, 100);
            Person.Dostarczone_cel = Person.BMR - Person.Spalone_cel;
            Preferences.Set("Spalone_Cel", Person.Spalone_cel.ToString());
            Preferences.Set("Dostarczone_Cel", Person.Dostarczone_cel.ToString());
            DependencyService.Get<IMessage>().ShortAlert("Zatwierdzono dane!");
        }
    }
}