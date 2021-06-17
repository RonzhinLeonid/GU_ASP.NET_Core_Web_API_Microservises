using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.DAL
{
    public class DateTimeOffsetСonverter : IValueConverter<long, DateTimeOffset>
    {
        public DateTimeOffset Convert(long sourceMember, ResolutionContext context)
        {
            return DateTimeOffset.FromUnixTimeSeconds(sourceMember);
        }
    }
}
