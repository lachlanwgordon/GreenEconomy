using System;
using System.Collections.Generic;
using GreenEconomy.Core;
using GreenEconomy.Core.ViewModels;
using Xamarin.Forms;
using DryIoc;
using System.Diagnostics;

namespace GreenEconomy.Forms.Views
{
    public partial class BusinessPage : BasePage<BusinessViewModel>
    {
        public BusinessPage()
        {
            InitializeComponent();
        }
    }
}
