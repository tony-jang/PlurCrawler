using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlurCrawler.Search
{
    /// <summary>
    /// PlurCrawler에서 제공하는 서비스 목록을 나타냅니다.
    /// </summary>
    public enum ServiceKind
    {
        /// <summary>
        /// Google Custom Search Engine을 나타냅니다.
        /// </summary>
        GoogleCSE,
        /// <summary>
        /// 유튜브를 나타냅니다.
        /// </summary>
        Youtube,
        /// <summary>
        /// 트위터를 나타냅니다.
        /// </summary>
        Twitter,
    }
}
