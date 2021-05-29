using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Responses
{
    public class NetworkMetricsByTimePeriodResponse
    {
        public List<NetworkMetricsResponse> Metrics { get; set; }
    }
}
