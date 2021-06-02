using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.DAL.Models
{
    public class AgentInfo
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public bool IsEnabled { get; set; }
    }
}
