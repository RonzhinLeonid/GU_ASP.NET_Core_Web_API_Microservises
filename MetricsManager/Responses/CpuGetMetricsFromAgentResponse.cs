using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Responses
{
    public class CpuGetMetricsFromAgentResponse
    {
        public IEnumerable<CpuMetricResponse> Metrics { get; set; }
    }
}
