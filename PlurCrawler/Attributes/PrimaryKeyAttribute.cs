using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlurCrawler.Attributes
{
    /// <summary>
    /// 해당 요소가 DataBase에서 기본키가 됨을 나타내는 특성입니다. 이 클래스는 상속될 수 없습니다.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class PrimaryKeyAttribute : Attribute
    {
    }
}
