using MetricsManager.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Services.Response
{
    public class AllDotNetMetricsApiResponse
    {
        public List<DotNetMetric> Metrics { get; set; }
    }
}
