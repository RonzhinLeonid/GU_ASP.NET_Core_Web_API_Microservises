﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Responses
{
    public class CpuMetricsResponse
    {
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
    }
}
