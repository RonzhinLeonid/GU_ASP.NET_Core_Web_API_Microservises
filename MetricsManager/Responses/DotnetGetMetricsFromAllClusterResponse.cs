using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Responses
{
    public class DotNetGetMetricsFromAllClusterResponse
    {
        public IEnumerable<DotNetMetricResponse> Metrics { get; set; }
    }
}
