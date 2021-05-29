using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Responses
{
    public class NetworkGetMetricsFromAllClusterResponse
    {
        public IEnumerable<NetworkMetricResponse> Metrics { get; set; }
    }
}
