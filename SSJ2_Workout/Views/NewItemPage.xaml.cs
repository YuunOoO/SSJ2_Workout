using System;
using System.Collections.Generic;
using System.ComponentModel;
using SSJ2_Workout.Models;
using SSJ2_Workout.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SSJ2_Workout.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}