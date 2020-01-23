using System;
using System.Collections.Generic;
using System.ComponentModel;
using GreenEconomy.Core.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GreenEconomy.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();

            Item = new Item
            {
                Text = "Item name",
                Description = "This is an item description."
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}