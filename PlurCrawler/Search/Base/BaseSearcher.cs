using System;
using System.Linq;
using System.Collections.Generic;

using PlurCrawler.Extension;
using PlurCrawler.Attributes;
using PlurCrawler.Search.Common;

namespace PlurCrawler.Search.Base
{
    /// <summary>
    /// 기본 서쳐입니다.
    /// </summary>
    public abstract class BaseSearcher<TOption, TResult> : ISearcher where TOption : ISearchOption
                                                                     where TResult : ISearchResult
    {
        /// <summary>
        /// 검색하려는 엔진이 인증되었는지에 대한 여부를 가져옵니다.
        /// </summary>
        public bool IsVerification { get; internal set; }

        /// <summary>
        /// 검색을 실시합니다.
        /// </summary>
        /// <param name="searchOption">검색 옵션입니다.</param>
        /// <returns></returns>
        public abstract IEnumerable<TResult> Search(TOption searchOption);
        
        public delegate void SearchResultDelegate(object sender);

        public event EventHandler<ProgressEventArgs> SearchProgressChanged;
        public event EventHandler<SearchFinishedEventArgs> SearchFinished;
        public event EventHandler<MessageEventArgs> ChangeInfoMessage;
        public event EventHandler<SearchResultEventArgs> SearchItemFound;

        internal void OnSearchProgressChanged(object sender, ProgressEventArgs e)
        {
            SearchProgressChanged?.Invoke(sender, e);
        }

        internal void OnSearchFinished(object sender, SearchFinishedEventArgs e)
        {
            SearchFinished?.Invoke(sender, e);
        }

        internal void OnChangeInfoMessage(object sender, MessageEventArgs e)
        {
            ChangeInfoMessage?.Invoke(sender, e);
        }

        internal void OnSearchItemFound(object sender, SearchResultEventArgs e)
        {
            SearchItemFound?.Invoke(sender, e);
        }
    }
}
