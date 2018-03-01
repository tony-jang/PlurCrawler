using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlurCrawler.Attributes
{
    /// <summary>
    /// 해당 속성은 저장할때에 무시할 속성임을 나타내는 특성입니다. 이 클래스는 상속될 수 없습니다.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class IgnorePropertyAttribute : Attribute
    {
    }
}
