using System.ComponentModel;
using SSJ2_Workout.ViewModels;
using Xamarin.Forms;

namespace SSJ2_Workout.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}