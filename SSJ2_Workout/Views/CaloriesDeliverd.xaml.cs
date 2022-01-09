using System;
using Xamarin.Essentials;
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
        }

        protected override async void OnAppearing()
        {
            
            base.OnAppearing();
            // collectionView.ItemsSource = await App.Database<Person>().GetProductAsync();
            var p = App<Product>.Database;
            collectionView.ItemsSource = await App<Product>.Database.GetProductAsync();
           if(SavedData.sounds)
            {
                var player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;

                player.Load("naruto_sad.mp3");
                player.Play();
                player.Loop = true;
            }
            CheckCalories2();

        }
        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
            if(SavedData.sounds)
            {
                Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current.Loop = false;
                Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current.Stop();
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

        async void CheckCalories(object sender, EventArgs e)
        {
            var suma = (MainViewModel)BindingContext;
            var produkty = await App<Product>.Database.GetProductAsync();
            int suma_tmp = 0;
            foreach (var produkt in produkty)
            {
                suma_tmp += produkt.Calories;
            }
            suma.Suma = suma_tmp;
        }

        public async void CheckCalories2()
        {
            var suma = (MainViewModel)BindingContext;
            var produkty = await App<Product>.Database.GetProductAsync();
            int suma_tmp = 0;
            int suma_tmp2 = 0;
            foreach (var produkt in produkty)
            {
                suma_tmp += produkt.Calories;
                if (produkt.Eat)
                    suma_tmp2 += produkt.Calories;
            }
            suma.Suma = suma_tmp;
            suma.Suma2 = suma_tmp2;
            SavedData.sum_save = suma_tmp;
            SavedData.sum2_save = suma_tmp2;
            Preferences.Set("SUM", $"{SavedData.sum_save}");
            Preferences.Set("SUM2", $"{SavedData.sum2_save}");
        }

        public static async void zeruj()
        {
            var products = await App<Product>.Database.GetProductAsync();
            foreach (var product in products)
            {
                product.Eat = false;
                await App<Exercise>.Database.UpdateProduct(product);
            }
            CaloriesDeliverd elo = new CaloriesDeliverd();
            elo.CheckCalories2();
        }

        async void OnChange(object sender, EventArgs e)
        {
            ImageButton button = sender as ImageButton;
            var product = button.BindingContext as Product;
           // CheckedChangedEventArgs klik = e as CheckedChangedEventArgs;
          //  bool klikniecie = klik.Value;
          //  if (klikniecie)
           // {
                if (!product.Eat)
                    product.Eat = true;
                else
                    product.Eat = false;

                await App<Product>.Database.UpdateProduct(product);
          //  }
            //Product robota
            //robota = await App.Database.GetProduct(product.Id);
            //////////////////////
            ///trzeba dopracowac jeszcze 
            CheckCalories2();
        }

        async void DeleteButtonClicked(object sender, EventArgs e)
        {

            ImageButton button = sender as ImageButton;
            var product = button.BindingContext as Product;

            await App<Product>.Database.DeleteProductAsync(product);
            collectionView.ItemsSource = await App<Product>.Database.GetProductAsync();  //no kurwa jestem genialny i tyle xdddd // ano jesteś -JK
            CheckCalories2();
        }
        async void OnButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(nameEntry.Text) && !string.IsNullOrWhiteSpace(caloriesEntry.Text))
            {
                if (IsDigitsOnly(caloriesEntry.Text))
                {
                    await App<Product>.Database.SaveProductAsync(new Product
                    {
                        Eat = eat.IsChecked,
                        Name = nameEntry.Text,
                        Calories = Convert.ToInt32(caloriesEntry.Text)
                    });

                    nameEntry.Text = string.Empty;
                    caloriesEntry.Text = string.Empty;
                    eat.IsChecked = false;
                    collectionView.ItemsSource = await App<Product>.Database.GetProductAsync();
                    CheckCalories2();
                    DependencyService.Get<IMessage>().ShortAlert("Pomyślnie dodano produkt!");
                }
                else
                {
                    DependencyService.Get<IMessage>().ShortAlert("Podano Niepoprawne dane ): ");
                }
            }
        }
    }
}