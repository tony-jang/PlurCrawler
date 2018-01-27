using PlurCrawler.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlurCrawler.Extension
{
    public static class EnumEx
    {
        public static T GetAttributeFromEnum<T>(this Enum value) where T : Attribute
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            T[] attributes = (T[])fi.GetCustomAttributes(typeof(T), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0];
            else
                return null;
        }

        public static T[] GetAttributesFromEnum<T>(this Enum value) where T : Attribute
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            T[] attributes = (T[])fi.GetCustomAttributes(typeof(T), false);

            if (attributes != null && attributes.Length > 0)
                return attributes;
            else
                return null;
        }
    }
}
