﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Text;

namespace EncounterMe.Pins
{
    [Serializable]
    public struct Address
    {
        public string country { get; set; }
        public string city { get; set; }
        public string postalCode { get; set; }
        public string street { get; set; }
    }
}
