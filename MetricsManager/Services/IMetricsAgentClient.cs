using MetricsManager.Responses;
using MetricsManager.Services.Requests;
using MetricsManager.Services.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Services
{
    public interface IMetricsAgentClient
    {
            AllRamMetricsApiResponse GetAllRamMetrics(GetAllRamMetricsApiRequest request);

            AllHddMetricsApiResponse GetAllHddMetrics(GetAllHddMetricsApiRequest request);

            AllDotNetMetricsApiResponse GetAllDotNetMetrics(GetAllDotNetHeapMetrisApiRequest request);

            AllCpuMetricsApiResponse GetAllCpuMetrics(GetAllCpuMetricsApiRequest request);

            AllNetworkMetricsApiResponse GetAllNetworkMetrics(GetAllNetworkMetricsApiRequest request);
    }
}