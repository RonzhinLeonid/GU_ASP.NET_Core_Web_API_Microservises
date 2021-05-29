using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Responses
{
    public class AgentsResponse
    {
        public int Id { get; set; }
        public string Uri { get; set; }
        public bool IsEnabled { get; set; }
    }
}
