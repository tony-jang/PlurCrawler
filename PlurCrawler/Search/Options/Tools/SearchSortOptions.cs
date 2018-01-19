﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlurCrawler.Search.Options.Tools
{
    /// <summary>
    /// 검색 결과의 정렬 결과를 결정합니다.
    /// </summary>
    public enum SearchSortOptions
    {
        /// <summary>
        /// 유사성 우선적으로 정렬합니다.
        /// </summary>
        []
        Similarity,
        /// <summary>
        /// 날짜의 최신 우선순으로 정렬합니다.
        /// </summary>
        Date
    }
}
