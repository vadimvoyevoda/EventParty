using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchModelLibrary
{
    public class SearchModel
    {
        public string Text { get; set; }
        public bool OnlyInTitles { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public bool IsCharitable { get; set; }
        public bool IsFree { get; set; }
        public string ToTime { get; set; }
        public string FromTime { get; set; }
        public string City { get; set; }
        public bool Filter { get; set; }
        public List<string> Types { get; set; }
        public List<string> Categories { get; set; }
    }
}
