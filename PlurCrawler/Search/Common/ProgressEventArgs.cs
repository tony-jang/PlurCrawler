using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlurCrawler.Search.Common
{
    public class ProgressEventArgs : EventArgs
    {
        public ProgressEventArgs(int maximum, int value)
        {
            this.Maximum = maximum;
            this.Value = value;
        }

        public int Maximum { get; }

        public int Value { get; }
    }
}
