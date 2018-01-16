using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using PlurCrawler.Search.Options;
using PlurCrawler.Search.SearchResults;
using PlurCrawler.Tokens.Tokenizer;

namespace PlurCrawler.Search
{
    public class NaverCafeSearcher : ISearcher
    {
        public IToken Token { get; private set; }

        public NaverToken NaverToken => (NaverToken)Token;

        public NaverCafeSearcher(NaverToken token)
        {
            this.Token = token;
        }

        public ISearchResult Search(ISearchOption searchOption)
        {
            if (searchOption is NaverSearchOption naverSearchOption)
            {
                string url = $"https://openapi.naver.com/v1/search/blog?query={naverSearchOption.Query}";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Headers.Add("X-Naver-Client-Id", NaverToken.ClientId);
                request.Headers.Add("X-Naver-Client-Secret", NaverToken.ClientSecret);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                HttpStatusCode status = response.StatusCode;

                if (status == HttpStatusCode.OK)
                {
                    Stream stream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(stream, Encoding.UTF8);

                    string text = reader.ReadToEnd();
                }
                else
                {
#if DEBUG
                    throw new Exception($"오류 발생 코드 : {status.ToString()}");
#endif
                }


                return null;
            }
            else
            {
                throw new SearchOptionTypeException($"{searchOption.GetType().ToString()} 타입이 아닌 'NaverSearchOption' 타입만 넣을 수 있습니다.");
            }
        }
    }
}
