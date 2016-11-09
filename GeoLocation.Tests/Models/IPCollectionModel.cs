using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace GeoLocation.Tests.Models
{
    public class IPCollection
    {
        public IEnumerable<Rule> IPs { get; set; }
    }

    public class Rule
    {
        public string country { get; set; }
        public string ip { get; set; }
    }
}
