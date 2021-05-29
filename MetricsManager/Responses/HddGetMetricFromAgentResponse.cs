using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Responses
{
    public class HddGetMetricFromAgentResponse
    {
        public IEnumerable<HddMetricResponse> Metrics { get; set; }
    }
}
