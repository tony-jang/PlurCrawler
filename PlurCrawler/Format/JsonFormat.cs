using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using PlurCrawler.Format.Base;
using PlurCrawler.Search.Base;

namespace PlurCrawler.Format
{
    /// <summary>
    /// json 형태로 반환하는 포맷을 나타냅니다.
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public class JsonFormat<TResult> : BaseFormat<TResult, string> where TResult : ISearchResult
    {
        /// <summary>
        /// <see cref="JsonFormat{TResult}"/>을 초기화합니다.
        /// </summary>
        /// <param name="indented">Json 파일을 빈칸 형태로 보기 좋게 정리할지에 대한 여부입니다.</param>
        public JsonFormat(bool indented)
        {
            this.Indented = indented;
        }

        /// <summary>
        /// Json 파일을 빈칸 형태로 보기 좋게 정리할지에 대한 여부입니다.
        /// </summary>
        public bool Indented { get; set; }

        /// <summary>
        /// <see cref="IEnumerable{T}"/> 형태의 데이터를 json 기반 string으로 반환합니다.
        /// </summary>
        /// <param name="resultData"></param>
        /// <returns></returns>
        public override string FormattingData(IEnumerable<TResult> resultData)
        {
            return JsonConvert.SerializeObject(resultData, (Indented ? Formatting.Indented : Formatting.None));
        }
    }
}
