using AutoMapper;
using System;

namespace MetricsAgent.DAL
{
    public class DateTimeOffsetСonverter : IValueConverter<long, DateTimeOffset>
    {
        public DateTimeOffset Convert(long sourceMember, ResolutionContext context)
        {
            return DateTimeOffset.FromUnixTimeSeconds(sourceMember);
        }
    }
}
