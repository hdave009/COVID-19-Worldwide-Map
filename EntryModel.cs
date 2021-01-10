using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COVID_19_Information
{
    public class EntryModel
    {
        public string Country { get; set; }
        public long TotalCases { get; set; }
        public long TotalDeaths { get; set; }
        public long TotalRecovered { get; set; }
        public long ActiveCases { get; set; }
    }
}
