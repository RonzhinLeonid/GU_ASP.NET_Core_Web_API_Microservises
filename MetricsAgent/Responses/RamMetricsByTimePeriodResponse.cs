using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Responses
{
    public class RamMetricsByTimePeriodResponse
    {
        public List<RamMetricsResponse> Metrics { get; set; }
    }
}
