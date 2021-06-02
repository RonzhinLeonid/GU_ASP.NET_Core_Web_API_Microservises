using MetricsManager.Responses;
using MetricsManager.Services.Requests;
using MetricsManager.Services.Response;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MetricsManager.Services
{
    public class MetricsAgentClient : IMetricsAgentClient
    {
        private readonly HttpClient httpClient;
        private readonly ILogger<MetricsAgentClient> _logger;
        public MetricsAgentClient(HttpClient client, ILogger<MetricsAgentClient> logger)
        {
            httpClient = client;
            _logger = logger;
        }

        public AllHddMetricsApiResponse GetAllHddMetrics(GetAllHddMetricsApiRequest request)
        {
            var fromParameter = request.FromTime;
            var toParameter = request.ToTime;
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.AgentUrl}/api/metrics/hdd/agent/from/{fromParameter}/to/{toParameter}");
            try
            {
                HttpResponseMessage response = httpClient.SendAsync(httpRequest).Result;

                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                return JsonSerializer.DeserializeAsync<AllHddMetricsApiResponse>(responseStream).Result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public AllRamMetricsApiResponse GetAllRamMetrics(GetAllRamMetricsApiRequest request)
        {
            var fromParameter = request.FromTime;
            var toParameter = request.ToTime;
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.AgentUrl}/api/metrics/ram/agent/from/{fromParameter}/to/{toParameter}");
            try
            {
                HttpResponseMessage response = httpClient.SendAsync(httpRequest).Result;

                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                return JsonSerializer.DeserializeAsync<AllRamMetricsApiResponse>(responseStream).Result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public AllCpuMetricsApiResponse GetAllCpuMetrics(GetAllCpuMetricsApiRequest request)
        {
            var fromParameter = request.FromTime;
            var toParameter = request.ToTime;
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.AgentUrl}/api/metrics/cpu/agent/from/{fromParameter}/to/{toParameter}");
            try
            {
                HttpResponseMessage response = httpClient.SendAsync(httpRequest).Result;

                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                return JsonSerializer.DeserializeAsync<AllCpuMetricsApiResponse>(responseStream).Result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public AllNetworkMetricsApiResponse GetAllNetworkMetrics(GetAllNetworkMetricsApiRequest request)
        {
            var fromParameter = request.FromTime;
            var toParameter = request.ToTime;
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.AgentUrl}/api/metrics/network/agent/from/{fromParameter}/to/{toParameter}");
            try
            {
                HttpResponseMessage response = httpClient.SendAsync(httpRequest).Result;

                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                return JsonSerializer.DeserializeAsync<AllNetworkMetricsApiResponse>(responseStream).Result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public AllDotNetMetricsApiResponse GetAllDotNetMetrics(GetAllDotNetHeapMetrisApiRequest request)
        {
            var fromParameter = request.FromTime;
            var toParameter = request.ToTime;
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.AgentUrl}/api/metrics/dotnet/agent/from/{fromParameter}/to/{toParameter}");
            try
            {
                HttpResponseMessage response = httpClient.SendAsync(httpRequest).Result;

                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                return JsonSerializer.DeserializeAsync<AllDotNetMetricsApiResponse>(responseStream).Result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }
    }
}
