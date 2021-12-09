using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    Person.Waga = Convert.ToInt32(wagaEntry.Text);
                    Person.Wzrost = Convert.ToInt32(wzrostEntry.Text);
                    Person.Wiek = Convert.ToInt32(wiekEntry.Text);
                    Person.Gender = genderPicker.Items[genderPicker.SelectedIndex];
                    Person.BMI = Decimal.Divide (Person.Waga , (Decimal.Divide(Person.Wzrost,100)* Decimal.Divide(Person.Wzrost , 100)));
                    
                    wzrostEntry.Text = string.Empty;
                    wiekEntry.Text = string.Empty;
                    wagaEntry.Text = string.Empty;
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
    }
}