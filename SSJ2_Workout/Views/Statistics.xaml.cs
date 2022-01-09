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
    public partial class Statistics : ContentPage
    {
        public Statistics()
        {
            InitializeComponent();
         
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            collectionView3.ItemsSource = await App<StoreData>.DatabaseStore.GetProductAsync();
        }

    }
}