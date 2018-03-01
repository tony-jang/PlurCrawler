using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlurCrawler.Attributes
{
    /// <summary>
    /// MySQL에서 사용하는 타입명을 나타내는 특성입니다. 이 클래스는 상속될 수 없습니다.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class MySQLTypeAttribute : Attribute
    {
        /// <summary>
        /// <see cref="MySQLTypeAttribute"/> 클래스를 초기화합니다.
        /// </summary>
        /// <param name="message"></param>
        public MySQLTypeAttribute(string typeString)
        {
            this.TypeString = typeString;
            
        }
        public string TypeString { get; set; }
    }
}
