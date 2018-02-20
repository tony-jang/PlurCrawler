using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlurCrawler.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    sealed class MySQLTypeAttribute : Attribute
    {
        public MySQLTypeAttribute(string typeString)
        {
            this.TypeString = typeString;
            
        }
        public string TypeString { get; set; }
    }
}
