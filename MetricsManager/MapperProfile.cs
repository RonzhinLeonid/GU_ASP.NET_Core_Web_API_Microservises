using AutoMapper;
using MetricsManager.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetricsManager.Responses;
using MetricsManager.DAL.Models;

namespace MetricsManager
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            var valueConverter = new DateTimeOffsetСonverter();
            CreateMap<AgentInfo, AgentsResponse>();
            CreateMap<RamMetric, CpuMetricResponse>()
            .ForMember(r => r.Time, exp => exp.ConvertUsing(valueConverter, val => val.Time));
            CreateMap<DotNetMetric, DotNetMetricResponse>()
            .ForMember(r => r.Time, exp => exp.ConvertUsing(valueConverter, val => val.Time));
            CreateMap<HddMetric, HddMetricResponse>()
            .ForMember(r => r.Time, exp => exp.ConvertUsing(valueConverter, val => val.Time));
            CreateMap<NetworkMetric, NetworkMetricResponse>()
            .ForMember(r => r.Time, exp => exp.ConvertUsing(valueConverter, val => val.Time));
            CreateMap<RamMetric, RamMetricResponse>()
            .ForMember(r => r.Time, exp => exp.ConvertUsing(valueConverter, val => val.Time));
        }
    }
}
