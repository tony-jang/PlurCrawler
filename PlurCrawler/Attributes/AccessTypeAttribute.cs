﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlurCrawler.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    sealed class AccessTypeAttribute : Attribute
    {
        public AccessTypeAttribute(string typeString)
        {
            this.TypeString = typeString;

        }
        public string TypeString { get; set; }
    }
}