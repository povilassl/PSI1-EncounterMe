﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EncounterMe.Pins;
using MvvmHelpers;

namespace EncounterMe.ViewModels
{
    class RoutePageViewModel : BaseViewModel
    {
        private StyleType _selectedStyleType;

        public StyleType SelectedStyleType
        {
            get
            {
                return _selectedStyleType;
            }
            set
            {
                SetProperty(ref _selectedStyleType, value);
            }
        }

        public List<string> StyleTypeNames
        {
            get
            {
                return Enum.GetNames(typeof(StyleType)).Select(b => b).ToList();
            }
        }

        private ObjectType _selectedObjectType;

        public ObjectType SelectedObjectType
        {
            get
            {
                return _selectedObjectType;
            }
            set
            {
                SetProperty(ref _selectedObjectType, value);
            }
        }

        public List<string> ObjectTypeNames
        {
            get
            {
                return Enum.GetNames(typeof(ObjectType)).Select(b => b).ToList();
            }
        }

    }
}
