# PlurCrawler
Crawler for BigData Collection
## Introduce Videos
[![Video](https://user-images.githubusercontent.com/23194065/37530966-2f2378d0-297e-11e8-9d19-5f3d9567e535.png)](https://youtu.be/5CrViIHzFCU)
## [Nuget](https://www.nuget.org/packages/PlurCrawler/)
```
PM> Install-Package PlurCrawler 
```
------
## 1. Google Custom Search Engine
### Google CSE Searcher Members
| Members                       | Member Kind       | Return Type                          |
| ----------------------------- | ----------------- | ------------------------------------ |
| Vertification(string, string) | Method            | void                                 |
| Search(GoogleCSESearchOption  | Method            | IEnumerable\<GoogleCSESearchResult\> |
| ApiKey                        | ReadOnly Property | string                               |
| SearchEngineId                | ReadOnly Property | string                               |
| IsVertification               | ReadOnly Property | bool                                 |
### GoogleCSE Search Options
| Name           | Type                |
| -------------- | ------------------- |
| Query          | string              |
| SearchCount    | int                 |
| Offset         | int                 |
| CountryCode    | CountryRestrictCode |
| DateRange      | DateRange           |
| SplitWithDate  | bool                |
| OutputServices | OutputFormat        |
| UseDateSearch  | bool                |

------
## 2. Youtube
### Youtube Searcher Members
| Members                       | Member Kind       | Return Type                          |
| ----------------------------- | ----------------- | ------------------------------------ |
| Vertification(string, string) | Method            | void                                 |
| Search(GoogleCSESearchOption) | Method            | IEnumerable\<GoogleCSESearchResult\> |
| ApiKey                        | ReadOnly Property | string                               |
| IsVertification               | ReadOnly Property | bool                                 |
### Youtube Search Option
| Name                 | Type                  |
| -------------------- | --------------------- |
| Query                | string                |
| SearchCount          | int                   |
| RegionCode           | RegionCode            |
| DateRange            | DateRange             |
| SplitWithDate        | bool                  |
| OutputServices       | OutputFormat          |
| UseDateSearch        | bool                  |
| YoutubeSortOrder     | YoutubeSortOrder?     |
| YoutubeVideoDuration | YoutubeVideoDuration? |

------

## 3. Twitter
### Twitter Searcher Members
| Members                        | Member Kind       | Return Type                           |
| ------------------------------ | ----------------- | ------------------------------------- |
| Search(TwitterCSESearchOption) | Method            | IEnumerable\<TwitterCSESearchResult\> |
| IsVertification                | ReadOnly Property | bool                                  |
### Twitter Search Option
| Name            | Type            |
| --------------- | --------------- |
| Query           | string          |
| SearchCount     | int             |
| Offset          | int             |
| Language        | TwitterLanguage |
| DateRange       | DateRange       |
| SplitWithDate   | bool            |
| OutputServices  | OutputFormat    |
| IncludeRetweets | bool            |

## Basic Usage
### Namespace Declare
```c#using PlurCrawler; // Base Namespaceusing PlurCrawler.Search.Services.GoogleCSE; // Google CSE Namespaceusing PlurCrawler.Search.Services.Youtube; // Youtube Namespaceusing PlurCrawler.Search.Services.Twitter; // Twitter Namespace```

### Youtube Search Example
```c#
var searcher = new YoutubeSearcher();
searcher.Vertification("YOUR-API-KEY");
var list = searcher.Search(new YoutubeSearchOption()
{
    Query = "YOUR-QUERY",
    YoutubeSortOrder = YoutubeSortOrder.Relevance,
    SearchCount = 50,
    RegionCode = PlurCrawler.Search.RegionCode.All,
    SplitWithDate = false,
    UseDateSearch = true,
    YoutubeVideoDuration = YoutubeVideoDuration.Any
});

foreach (var itm in list){
    Console.WriteLine($"【{itm.Title}】 - {itm.SimplifyDescription}");
    Console.WriteLine();
}
```

### Twitter Search Example
```C#
TwitterCredentials credentials = new TwitterCredentials("YOUR-CONSUMER-KEY", "YOUR_CONSUMER-SECRET");
TwitterTokenizer tokenizer = new TwitterTokenizer();

Process.Start(tokenizer.GetURL(credentials));

Console.Write("Input PIN CODE : ");
string pinCode = Console.ReadLine();
credentials.InputPIN(pinCode);
tokenizer.CredentialsCertification(credentials);

var list = searcher.Search(new TwitterSearchOption(){
    Query = "YOUR-QUERY",
    SplitWithDate = false,
    IncludeRetweets = true,
    Language = TwitterLanguage.All,
    SearchCount = 10
});

foreach(var itm in list)
{
    Console.WriteLine($"【{itm.CreatorName}】 - {itm.SimplifyContent}");
    Console.WriteLine();
}
```

### Google CSE Search Example
```C#
var searcher = new GoogleCSESearcher("YOUR-CONSUMER-KEY", "YOUR_CONSUMER-SECRET");
var list = searcher.Search(new GoogleCSESearchOption(){    
    Query = "YOUR-QUERY",
    SearchCount = 50,
    CountryCode = PlurCrawler.Search.Common.CountryRestrictsCode.All,
    SplitWithDate = false,
    UseDateSearch = true
});

foreach (var itm in list)
{
    Console.WriteLine($"【{itm.Title}】- {itm.SimplifySnippet}");
    Console.WriteLine();
}
```


## Main Features
### 1. All Settings are Auto-Saved
Your infos are auto-saved when you edit. So you desn't need to type same info.<br/><br/>
### 2. Provides Many Services
You can use Google CSE, Twitter, Youtube Services when you use PlurCrawler. I'll add another engine that can be serviced.<br/><br/>

### 3. Remember Tasks
If you failed to export, check why you failed and helps export again and you don't need to search again.<br/><br/>

### 4. Divided Library and Execute File
PlurCrawler is Divided Library and Execute File. so if you use PlurCrawler as Developer, you can use Library. Otherwise, if you are normal User that want to collect datas, you can use tool.<br/><br/>

### 5. Provides variety Export Environment.
It Provides variety Export Environment Like Json, CSV, MySQL, Access DB etc.. You can select one or more export engines.<br/><br/>

### 6. Can download easy at Nuget
Download 'PlurCrawler' at Nuget, You're all ready to use 'PlurCrawler' Library<br/><br/>
