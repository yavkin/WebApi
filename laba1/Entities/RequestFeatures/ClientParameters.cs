﻿using System;
using System.Collections.Generic;
using System.Text;
using Entities.RequestFeatures;

namespace Entities.RequestFeatures
{
    public class ClientParameters : RequestParameters
    {
        public ClientParameters()
        {
            OrderBy = "name";
        }
        public uint MinAge { get; set; }
        public uint MaxAge { get; set; } = int.MaxValue;
        public bool ValidAgeRange => MaxAge > MinAge;
    }
}