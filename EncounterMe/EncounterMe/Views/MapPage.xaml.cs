﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Xamarin.Forms.Maps;
using System.Diagnostics;
using EncounterMe.Services;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Services;
using EncounterMe.Views.Popups;

//possible to implement route tracking https://www.xamboy.com/2019/05/17/exploring-map-tracking-ui-in-xamarin-forms/
//https://www.xamboy.com/2019/05/29/google-maps-place-search-in-xamarin-forms/

//TODO: change DisplayAlert to sth else, it doesnt close after opening settings


namespace EncounterMe.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {

        public bool chosenLocationPermission;
        public bool chosenGpsPermission;

        public MapPage()
        {
            chosenLocationPermission = false;
            chosenGpsPermission = false;

            InitializeComponent();
            DisplayCurrentLocation();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            DisplayCurrentLocation();
        }
        
        private async void DisplayCurrentLocation()
        {
            try
            {
                //Trying to display current location. GeolocationAccuracy.Default for quicker initialization
                var request = new GeolocationRequest(GeolocationAccuracy.Default);
                var location = await Geolocation.GetLocationAsync(request);

                if (location != null)
                {
                    //Moving to location and starting live tracking
                    MoveToLocation(location);
                    TrackingLiveLocation();
                }
            }
            catch (FeatureNotSupportedException featureNotSupportedException)
            {

            }
            catch (FeatureNotEnabledException featureNotEnabledException)
            {
                //If user was not yet prompted to enable gps
                if (chosenGpsPermission == false)
                {
                    PromptToEnableGPS();
                }
            }
            catch (PermissionException permissionException)            
            {
                //If location permission is not already enabled and not yet chosen, prompt the user and call the method once again
                if (chosenLocationPermission == false)
                {
                    PromptToEnableLocationPermission();
                }
            }
            catch (Exception exception)
            {

            }
        }
        async void PromptToEnableLocationPermission()
        {
            chosenLocationPermission = true;
            await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            DisplayCurrentLocation();
        }

        async void PromptToEnableGPS()
        {
            //If there was no button was pressed (tapped outside the alert box), answer = false, user will not get live tracking
            bool answer = await DisplayAlert("Location request", "Turn on your phone's location service for better performance.", "OK", "Maybe later");
            chosenGpsPermission = true;

            if (answer == true)
            {
                //Opens settings and starts tracking live location
                IGpsDependencyService GpsDependency = DependencyService.Get<IGpsDependencyService>();
                GpsDependency.OpenSettings();
                DisplayCurrentLocation();
            }
        }

        //Constantly runs in the background, tracks current location and updates it
        private void TrackingLiveLocation()
        {
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                Task.Run(async () =>
                {
                    var request = new GeolocationRequest(GeolocationAccuracy.Best);
                    var location = await Geolocation.GetLocationAsync(request);
                    Position p = new Position(location.Latitude, location.Longitude);
                    MapSpan mapSpan = MapSpan.FromCenterAndRadius(p, Distance.FromKilometers(1));
                });
                return true;
            });
        }

        //Moves map to given location
        void MoveToLocation(Location position)
        {
            Position p = new Position(position.Latitude, position.Longitude);
            MapSpan mapSpan = MapSpan.FromCenterAndRadius(p, Distance.FromKilometers(1));
            MyMap.MoveToRegion(mapSpan);
        }

        async void Add_Button_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new AddObjectPopup());
        }

        public string centerPinLatitude;
        public string centerPinLongitude;


        //Prints current center position.
        async void Add_Pin_Button_Clicked(object sender, EventArgs e)
        {
            Console.WriteLine("centerPinLatitude = " + centerPinLatitude);
            Console.WriteLine("centerPinLongitude = " + centerPinLongitude);
        }


        //Updates current center position when map is moved
        void Position_Map_Property_Changed(object sender, EventArgs e)
        {

            var m = (Xamarin.Forms.Maps.Map)sender;
            if (m.VisibleRegion != null)
            {            
                centerPinLatitude = m.VisibleRegion.Center.Latitude.ToString();
                centerPinLongitude = m.VisibleRegion.Center.Longitude.ToString();
            }
        }
    }
}
