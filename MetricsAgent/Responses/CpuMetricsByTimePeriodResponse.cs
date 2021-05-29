using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Responses
{
    public class CpuMetricsByTimePeriodResponse
    {
        public List<CpuMetricsResponse> Metrics { get; set; }
    }
}
