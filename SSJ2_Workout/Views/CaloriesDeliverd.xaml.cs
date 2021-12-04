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
    public partial class CaloriesDeliverd : ContentPage
    {

        public int suma { get; set; }
        public CaloriesDeliverd()
        {
            InitializeComponent();
            BindingContext = this;
            var suma = (CaloriesDeliverd)BindingContext;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            collectionView.ItemsSource = await App.Database.GetProductAsync();
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

        async void CheckCalories(object sender, EventArgs e)
        {
            var produkty = await App.Database.GetProductAsync();
            foreach(var produkt in produkty)
            {
                suma += produkt.Calories;
            }
            testowy.Text = $"Suma kalori to: {suma}";
        }

        async void DeleteButtonClicked(object sender, EventArgs e)
        {

            ImageButton button = sender as ImageButton;
            var product = button.BindingContext as Product;

            await App.Database.DeleteProductAsync(product);
            await Navigation.PopAsync();

        }

        async void OnButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(nameEntry.Text) && !string.IsNullOrWhiteSpace(caloriesEntry.Text))
            {
                if (IsDigitsOnly(caloriesEntry.Text))
                {
                    await App.Database.SaveProductAsync(new Product
                    {
                        Name = nameEntry.Text,
                        Calories = Convert.ToInt32(caloriesEntry.Text)
                    });

                    nameEntry.Text = string.Empty;
                    caloriesEntry.Text = string.Empty;

                    collectionView.ItemsSource = await App.Database.GetProductAsync();
                }
                else
                {
                    // bedzie kod powiadomienia ze zle podano
                }
            }
        }
    }
}