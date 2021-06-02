using MetricsManager.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Services.Response
{
    public class AllCpuMetricsApiResponse
    {
        public List<CpuMetric> Metrics { get; set; }
    }
}
