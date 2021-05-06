using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Responses
{
    public class RamMetricsResponse
    {
        long _time;
        public DateTimeOffset Time 
        { 
            get
            {
                return new DateTimeOffset(_time, new TimeSpan(0, 0, 0));
            }
        }
        public int Value { get; set; }
    }
}
