using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlurCrawler.Format
{
    public struct TableField
    {
        public TableField(string name, string type, bool isPrimary)
        {
            this.Name = name;
            this.Type = type;
            this.IsPrimary = isPrimary;
        }

        public string Name { get; set; }

        public string Type { get; set; }

        public bool IsPrimary { get; set; }
    }
}
