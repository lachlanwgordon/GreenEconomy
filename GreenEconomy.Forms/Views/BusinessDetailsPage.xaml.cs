using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace GreenEconomy.Forms.Views
{
    public partial class BusinessDetailsPage : ContentPage
    {
        public BusinessDetailsPage()
        {
            InitializeComponent();
            Debug.WriteLine($"BindingContext is: {BindingContext}");
        }
    }
}
