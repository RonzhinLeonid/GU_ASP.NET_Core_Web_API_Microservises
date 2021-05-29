using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Responses
{
    public class HddMetricsByTimePeriodResponse
    {
        public List<HddMetricsResponse> Metrics { get; set; }
    }
}
