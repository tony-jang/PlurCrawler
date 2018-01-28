using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PlurCrawler.Search;

namespace PlurCrawler.Attributes
{
    public class LanguageNoteAttribute : Attribute
    {
        public LanguageNoteAttribute(ServiceKind serviceKind, string languageString)
        {
            this.LanguageString = languageString;
            this.ServiceKind = serviceKind;
        }

        public ServiceKind ServiceKind { get; }
        public string LanguageString { get; }
    }
}
