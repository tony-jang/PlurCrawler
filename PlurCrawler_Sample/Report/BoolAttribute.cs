using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlurCrawler_Sample.Report
{
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    sealed class BoolAttribute : Attribute
    {
        public BoolAttribute(bool value)
        {
            this.Value = value;
        }

        public bool Value { get; }
    }
}
