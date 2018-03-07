using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace PlurCrawler.Crawling
{
    public class ContentCrawler
    {
        
        public ContentCrawler(string url)
        {
            try
            {
                WebClient wc = new WebClient();

                if (Regex.IsMatch(url, @"^(https{0,1}:\/\/)*m\."))
                {
                    wc.Headers.Add("user-agent", "Opera/12.02 (Android 4.1; Linux; Opera Mobi/ADR-1111101157; U; en-US) Presto/2.9.201 Version/12.02");
                }

                try
                {
                    byte[] docBytes = wc.DownloadData(url);
                    string encodeType = wc.ResponseHeaders["Content-Type"];
                
                    string charsetKey = "charset";
                    int pos = encodeType.IndexOf(charsetKey);
                
                    Encoding currentEncoding = Encoding.Default;
                    if (pos != -1)
                    {
                        pos = encodeType.IndexOf("=", pos + charsetKey.Length);
                        if (pos != -1)
                        {
                            string charset = encodeType.Substring(pos + 1);
                            currentEncoding = Encoding.GetEncoding(charset);
                        }
                    }
                
                    Document = currentEncoding.GetString(docBytes);
                    Content = GetContent(Document);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    Document = string.Empty;
                    Content = string.Empty;
                }
                
            }
            catch (IOException)
            {
            }
        }

        public string Document { get; set; }
        public string Content { get; set; }
        
        private string GetContent(string htmlText)
        {
            string[] tags = { "script", "style", "li", "footer", "noscript", "dt", "dd" , "header", "aside", "form"};

            string resultText = htmlText.Trim();

            foreach(string tag in tags)
            {
                resultText = Regex.Replace(resultText, $@"(<{tag}.*?>[\s\S]*?<\/{tag}>)", string.Empty, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            }
            resultText = Regex.Replace(resultText, @"<!--[\w\W]*?-->", "", RegexOptions.Multiline);
            resultText = Regex.Replace(resultText, $@"(<div .*?footer.*?>[\s\S]*?<\/div>)", "", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            resultText = Regex.Replace(resultText, $@"(<a[^r].*?>[\s\S]*?<\/a>)", string.Empty, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            resultText = Regex.Replace(resultText, "<[^>]*>", "\n");
            resultText = Regex.Replace(resultText, @"([\n\r\t] *)+", Environment.NewLine, RegexOptions.Multiline).Trim();

            return HttpUtility.HtmlDecode(resultText);
        }
    }
}
