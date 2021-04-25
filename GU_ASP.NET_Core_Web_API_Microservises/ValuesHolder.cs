using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GU_ASP.NET_Core_Web_API_Microservises
{
    public class ValuesHolder
    {
        List<string> values = new List<string>();
        public List<string> Values { get; set; }

        public void AddValue(string value)
        {
            values.Add(value);
        }
    }
}
