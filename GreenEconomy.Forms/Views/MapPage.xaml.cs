using System;
using System.Collections.Generic;
using System.Diagnostics;
using GreenEconomy.Core;
using GreenEconomy.Core.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace GreenEconomy.Forms.Views
{
    public partial class MapPage : BasePage<MapViewModel>
    {
        public MapPage()
        {
            InitializeComponent();
            TheMap.UiSettings.MyLocationButtonEnabled = true;
            //TheMap.InitialCameraUpdate = CameraUpdateFactory.NewCameraPosition(new CameraPosition(new Position(-37, 145), 5));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Debug.WriteLine(ViewModel.Businesses.Count);
            //var userLoc = await Xamarin.Essentials.Geolocation.GetLocationAsync();
            //await Shell.Current.DisplayAlert("hello", $"You are at {userLoc.Latitude},{userLoc.Longitude}", "okay");
        }
    }
}
