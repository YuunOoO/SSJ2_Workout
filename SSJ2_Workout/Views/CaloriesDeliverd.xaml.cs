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
        public CaloriesDeliverd()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
            CheckCalories2();
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
            var suma = (MainViewModel)BindingContext;
            var produkty = await App.Database.GetProductAsync();
            int suma_tmp = 0;
            foreach (var produkt in produkty)
            {
                suma_tmp += produkt.Calories;
            }
            suma.Suma = suma_tmp;
           // testowy.Text = $"Suma kalori to: {suma.Suma}";
        }

        public async void CheckCalories2()
        {
            var suma = (MainViewModel)BindingContext;
            var produkty = await App.Database.GetProductAsync();
            int suma_tmp = 0;
            foreach (var produkt in produkty)
            {
                suma_tmp += produkt.Calories;
            }
            suma.Suma = suma_tmp;
            SavedData.sum_save = suma_tmp;
           // testowy.Text = $"Suma kalori to: {suma.Suma}";
        }
        //MainViewModel CheckCalories3()
        //{
        //    return suma;
        //}

        async void DeleteButtonClicked(object sender, EventArgs e)
        {

            ImageButton button = sender as ImageButton;
            var product = button.BindingContext as Product;

            await App.Database.DeleteProductAsync(product);
            collectionView.ItemsSource = await App.Database.GetProductAsync();  //no kurwa jestem genialny i tyle xdddd
            CheckCalories2();
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
                    CheckCalories2();
                }
                else
                {
                    // bedzie kod powiadomienia ze zle podano
                }
            }
        }
    }
}