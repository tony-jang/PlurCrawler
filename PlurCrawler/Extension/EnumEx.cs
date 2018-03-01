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
        /// <summary>
        /// Enum으로부터 <see cref="{T}"/> 특성을 가져옵니다.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T GetAttributeFromEnum<T>(this Enum value) where T : Attribute
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            T[] attributes = (T[])fi.GetCustomAttributes(typeof(T), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0];
            else
                return null;
        }

        /// <summary>
        /// <see cref="{T}"/> 특성으로부터 Enum을 가져옵니다.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T[] GetAttributesFromEnum<T>(this Enum value) where T : Attribute
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            T[] attributes = (T[])fi.GetCustomAttributes(typeof(T), false);

            if (attributes != null && attributes.Length > 0)
                return attributes;
            else
                return null;
        }
        
        /// <summary>
        /// Enum의 모든 값들을 <see cref="IEnumerable{T}"/> 형태로 가져옵니다.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
        
    }
}
