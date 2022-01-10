using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace SSJ2_Workout.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CaloriesBurned : ContentPage
    {
        public CaloriesBurned()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();
            collectionView2.ItemsSource = await App<Exercise>.DatabaseExercise.GetProductAsync();
            if (SavedData.sounds)
            {
                var player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;

                player.Load("guts.mp3");
                player.Play();
                player.Loop = true;
            }
            CheckCalories3();
        }
        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
            if (SavedData.sounds)
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

        async void DeleteButtonClicked(object sender, EventArgs e)
        {

            ImageButton button = sender as ImageButton;
            var exercise = button.BindingContext as Exercise;

            await App<Exercise>.DatabaseExercise.DeleteProductAsync(exercise);
            collectionView2.ItemsSource = await App<Exercise>.DatabaseExercise.GetProductAsync();
            CheckCalories3();
        }
        async void OnButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(nameEntry.Text) && !string.IsNullOrWhiteSpace(caloriesEntry.Text))
            {
                if (IsDigitsOnly(caloriesEntry.Text))
                {
                    await App<Exercise>.DatabaseExercise.SaveProductAsync(new Exercise
                    {
                        Did = did.IsChecked,
                        Name = nameEntry.Text,
                        Calories = Convert.ToInt32(caloriesEntry.Text)
                    });

                    nameEntry.Text = string.Empty;
                    caloriesEntry.Text = string.Empty;
                    did.IsChecked = false;
                    collectionView2.ItemsSource = await App<Exercise>.DatabaseExercise.GetProductAsync();
                    CheckCalories3();
                    DependencyService.Get<IMessage>().ShortAlert("Pomyślnie dodano produkt!");
                }
                else
                {
                    DependencyService.Get<IMessage>().ShortAlert("Podano Niepoprawne dane ): ");
                }
            }
        }

        async void OnChange(object sender, EventArgs e)
        {
            ImageButton button = sender as ImageButton;
            var exercise = button.BindingContext as Exercise;
            if (!exercise.Did)
                exercise.Did = true;
            else
                exercise.Did = false;

            await App<Exercise>.DatabaseExercise.UpdateProduct(exercise);
            CheckCalories3();
        }

        public  async void CheckCalories3()
        {
            var suma = (MainViewModel)BindingContext;
            var exercises = await App<Exercise>.DatabaseExercise.GetProductAsync();
            int suma_tmp = 0;
            int suma_tmp2 = 0;
            foreach (var exercise in exercises)
            {
                suma_tmp += exercise.Calories;
                if (exercise.Did)
                    suma_tmp2 += exercise.Calories;
            }
            suma.Suma3 = suma_tmp;
            suma.Suma4 = suma_tmp2;
            SavedData.sum3_save = suma_tmp;
            SavedData.sum4_save = suma_tmp2;
            Preferences.Set("SUM3", $"{SavedData.sum3_save}");
            Preferences.Set("SUM4", $"{SavedData.sum4_save}");
        }

        public static async void zeruj()
        {
            var exercises = await App<Exercise>.DatabaseExercise.GetProductAsync();
            foreach (var exercise in exercises)
            {
                    exercise.Did = false;
                await App<Exercise>.DatabaseExercise.UpdateProduct(exercise);
            }
            CaloriesBurned elo = new CaloriesBurned();
            elo.CheckCalories3();
        }
        public void GoToMenu(object obj, EventArgs args)
        {
            Navigation.PushAsync(new PasekBoczny());
        }
    }
}