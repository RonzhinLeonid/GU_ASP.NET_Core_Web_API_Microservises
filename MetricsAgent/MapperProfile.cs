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
            var valueConverter = new DateTimeOffsetСonverter();

            CreateMap<CpuMetric, CpuMetricsResponse>()
            .ForMember(r => r.Time, exp => exp.ConvertUsing(valueConverter, val => val.Time));
            CreateMap<DotNetMetrics, DotNetMetricsResponse>()
            .ForMember(r => r.Time, exp => exp.ConvertUsing(valueConverter, val => val.Time));
            CreateMap<HddMetrics, HddMetricsResponse>()
            .ForMember(r => r.Time, exp => exp.ConvertUsing(valueConverter, val => val.Time));
            CreateMap<NetworkMetrics, NetworkMetricsResponse>()
            .ForMember(r => r.Time, exp => exp.ConvertUsing(valueConverter, val => val.Time));
            CreateMap<RamMetrics, RamMetricsResponse>()
            .ForMember(r => r.Time, exp => exp.ConvertUsing(valueConverter, val => val.Time));
        }
    }
}
