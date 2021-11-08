﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using EncounterMe;
using EncounterMe.Pins;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;


namespace EncounterMe
{
    class PinsList
    {

        private static PinsList s_instance;

        private static readonly object s_locker = new object();

        public List<MapPin> ListOfPins = new List<MapPin>();

        private CheckAddressCommands _checkAddressCommands = new CheckAddressCommands();

        private static readonly string s_filePath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "pins.bin");

        private protected PinsList()
        {

        }

        public static PinsList GetPinsList()
        {
            if (s_instance == null)
            {
                lock (s_locker)
                {
                    if (s_instance == null)
                    {
                        s_instance = new PinsList();
                    }
                }
            }
            return s_instance;
        }

        //tikriausiai nebe naudojami tai užkomentuoti
        /*public async void AddPinByAddressToList(MapPin mapPin)
        {
            await _checkAddressCommands.GetCoordinatesFromAddress(mapPin.Address);
            ListOfPins.Add(mapPin);

            if (mapPin.Latitude != 0 && mapPin.Longitude != 0)
                mapPin.CreateAPin();
        }

        public async void AddPinByAddressToList(string name, Address address, int type, int style, string description, WorkingHours hours, Image photo)
        {
            await _checkAddressCommands.GetCoordinatesFromAddress(address);

            MapPin newOne = new MapPin(name, description, address, type, style,
                                       hours, _checkAddressCommands.Location.Latitude, _checkAddressCommands.Location.Longitude, "no name");

            ListOfPins.Add(newOne);

            if(newOne.Latitude != 0 && newOne.Longitude != 0)
                newOne.CreateAPin();

           
        }
        public void AddPinByCoordinatesToList(MapPin mapPin)
        {
            ListOfPins.Add(mapPin);
            if (mapPin.Latitude != 0 && mapPin.Longitude != 0)
                mapPin.CreateAPin();
        }

        public void AddPinByCoordinatesToList(string name, Address address, Location location, int type, int style, string description, WorkingHours hours, Image photo)
        {
            MapPin newOne = new MapPin(name, description, address, type, style, hours,
                                      _checkAddressCommands.Location.Latitude, _checkAddressCommands.Location.Longitude, "no name");

            ListOfPins.Add(newOne);
            newOne.CreateAPin();
          
        }*/

    }
}
