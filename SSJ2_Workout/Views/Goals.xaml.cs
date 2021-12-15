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
            goalPicker.Items.Add("Szybkie schudnięcie.");
            goalPicker.Items.Add("Schudnięcie.");
            goalPicker.Items.Add("Utrzymanie wagi.");
            goalPicker.Items.Add("Przybranie na wadze.");
            goalPicker.Items.Add("Szybkie przybranie na wadze.");

        }
        private void goalChoice(object sender, EventArgs e)
        {
            string name = goalPicker.Items[goalPicker.SelectedIndex];
            Name = name;
        }
    }
}