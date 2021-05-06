using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MetricsAgent.DAL;
using MetricsAgent.Responses;

namespace MetricsAgent
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CpuMetric, CpuMetricsResponse>();
            CreateMap<DotNetMetrics, DotNetMetricsByTimePeriodResponse>();
            CreateMap<HddMetrics, HddMetricsByTimePeriodResponse>();
            CreateMap<NetworkMetrics, NetworkMetricsByTimePeriodResponse>();
            CreateMap<RamMetrics, RamMetricsByTimePeriodResponse>();
        }
    }
}
