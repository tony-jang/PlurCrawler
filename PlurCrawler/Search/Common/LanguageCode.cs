using PlurCrawler.Attributes;

namespace PlurCrawler.Search
{
    /// <summary>
    /// 언어 코드입니다.
    /// </summary>
    public enum LanguageCode
    {
        /// <summary>
        /// 아프리카어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "af")]
        Afrikaans,
        /// <summary>
        /// 알바니아어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "sq")]
        Albanian,
        /// <summary>
        /// 암하라어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "sm")]
        Amharic,
        /// <summary>
        /// 아라비아어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "ar")]
        Arabic,
        /// <summary>
        /// 아제르바이잔어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "az")]
        Azerbaijani,
        /// <summary>
        /// 바스크어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "eu")]
        Basque,
        /// <summary>
        /// 벨라루스어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "be")]
        Belarusian,
        /// <summary>
        /// 벵골어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "bn")]
        Bengali,
        /// <summary>
        /// 비하르어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "bh")]
        Bihari,
        /// <summary>
        /// 보스니아어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "bs")]
        Bosnian,
        /// <summary>
        /// 불가리아어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "bg")]
        Bulgarian,
        /// <summary>
        /// 카탈로니아어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "ca")]
        Catalan,
        /// <summary>
        /// 중국어 간체 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "zh-CN")]
        Chinese_Simplified,
        /// <summary>
        /// 중국어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "zh-TW")]
        Chinese_Traditional,
        /// <summary>
        /// 크로아티아어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "hr")]
        Croatian,
        /// <summary>
        /// 체코어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "cs")]
        Czech,
        /// <summary>
        /// 덴마크어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "da")]
        Danish,
        /// <summary>
        /// 네덜란드어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "nl")]
        Dutch,
        /// <summary>
        /// 영어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "en")]
        English,
        /// <summary>
        /// 에스페란토어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "eo")]
        Esperanto,
        /// <summary>
        /// 에스토니아어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "et")]
        Estonian,
        /// <summary>
        /// 페로어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "fo")]
        Faroese,
        /// <summary>
        /// 핀란드어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "fi")]
        Finnish,
        /// <summary>
        /// 프랑스어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "fr")]
        French,
        /// <summary>
        /// 프리지아어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "fy")]
        Frisian,
        /// <summary>
        /// 갈리시아어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "gl")]
        Galician,
        /// <summary>
        /// 조르지아어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "ka")]
        Georgian,
        /// <summary>
        /// 독일어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "de")]
        German,
        /// <summary>
        /// 그리스어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "el")]
        Greek,
        /// <summary>
        /// 구자라트어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "gu")]
        Gujarati,
        /// <summary>
        /// 헤브라이어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "iw")]
        Hebrew,
        /// <summary>
        /// 힌디어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "hi")]
        Hindi,
        /// <summary>
        /// 헝가리어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "hu")]
        Hungarian,
        /// <summary>
        /// 아이슬란드어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "is")]
        Icelandic,
        /// <summary>
        /// 인도네시아어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "id")]
        Indonesian,
        /// <summary>
        /// 인테르링구아어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "ia")]
        Interlingua,
        /// <summary>
        /// 아일랜드어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "ga")]
        Irish,
        /// <summary>
        /// 이탈리아어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "it")]
        Italian,
        /// <summary>
        /// 일본어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "ja")]
        Japanese,
        /// <summary>
        /// 자바어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "jw")]
        Javanese,
        /// <summary>
        /// 칸나다어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "kn")]
        Kannada,
        /// <summary>
        /// 한국어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "ko")]
        Korean,
        /// <summary>
        /// 라틴어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "la")]
        Latin,
        /// <summary>
        /// 라트비아어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "lv")]
        Latvian,
        /// <summary>
        /// 리투아니아어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "lt")]
        Lithuanian,
        /// <summary>
        /// 마케도니아어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "mk")]
        Macedonian,
        /// <summary>
        /// 말레이어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "ms")]
        Malay,
        /// <summary>
        /// 말라얄람어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "ml")]
        Malayam,
        /// <summary>
        /// 몰타어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "mt")]
        Maltese,
        /// <summary>
        /// 마라티어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "mr")]
        Marathi,
        /// <summary>
        /// 네팔어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "ne")]
        Nepali,
        /// <summary>
        /// 노르웨이어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "no")]
        Norwegian,
        /// <summary>
        /// 노르웨이 뉘노르스크어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "nn")]
        Norwegian_Nynorsk,
        /// <summary>
        /// 오크어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "oc")]
        Occitan,
        /// <summary>
        /// 페르시안어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "fa")]
        Persian,
        /// <summary>
        /// 폴리시어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "pl")]
        Polish,
        /// <summary>
        /// 브라질 포르투갈어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "pt-BR")]
        Portuguese_Brazil,
        /// <summary>
        /// 포르투갈어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "pt-PT")]
        Portuguese_Portugal,
        /// <summary>
        /// 펀자브어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "pa")]
        Punjabi,
        /// <summary>
        /// 루마니아어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "ro")]
        Romanian,
        /// <summary>
        /// 러시아어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "ru")]
        Russian,
        /// <summary>
        /// 스코틀랜드 게일어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "gd")]
        Scots_Gaelic,
        /// <summary>
        /// 세르비아어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "sr")]
        Serbian,
        /// <summary>
        /// 싱할라어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "si")]
        Sinhalese,
        /// <summary>
        /// 슬로바키아어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "sk")]
        Slovak,
        /// <summary>
        /// 슬로베니아어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "sl")]
        Slovenian,
        /// <summary>
        /// 스페인어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "es")]
        Spanish,
        /// <summary>
        /// 수단어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "su")]
        Sudanese,
        /// <summary>
        /// 스와힐리어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "sw")]
        Swahili,
        /// <summary>
        /// 스웨덴어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "sv")]
        Swedish,
        /// <summary>
        /// 타갈로그어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "tl")]
        Tagalog,
        /// <summary>
        /// 타밀어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "ta")]
        Tamil,
        /// <summary>
        /// 텔루구어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "te")]
        Telugu,
        /// <summary>
        /// 태국어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "th")]
        Thai,
        /// <summary>
        /// 티그리냐어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "ti")]
        Tigrinya,
        /// <summary>
        /// 터키어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "tr")]
        Turkish,
        /// <summary>
        /// 우크라이나어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "uk")]
        Ukrainian,
        /// <summary>
        /// 우르두어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "ur")]
        Urdu,
        /// <summary>
        /// 우즈베키스탄어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "uz")]
        Uzbek,
        /// <summary>
        /// 베트남어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "vi")]
        Vietnamese,
        /// <summary>
        /// 웨일스어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "cy")]
        Welsh,
        /// <summary>
        /// 코사어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "xh")]
        Xhosa,
        /// <summary>
        /// 줄루어 입니다.
        /// </summary>
        [LanguageNote(ServiceKind.GoogleCSE, "zu")]
        Zulu,
        All,
    }
}
