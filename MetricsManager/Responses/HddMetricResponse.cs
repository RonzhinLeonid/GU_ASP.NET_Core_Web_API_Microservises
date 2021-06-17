using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Responses
{
    public class HddMetricResponse
    {
        public int Id { get; set; }
        public int AgentId { get; set; }
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
    }
}
