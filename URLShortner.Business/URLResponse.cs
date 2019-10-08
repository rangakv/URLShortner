using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLShortner.Business
{
    public class URLResponse
    {
        public string kind { get; set; }
        public string id { get; set; }
        public string longUrl { get; set; }
        public string shorturl { get; set; }
        public string hashurl { get; set; }
    }
}
