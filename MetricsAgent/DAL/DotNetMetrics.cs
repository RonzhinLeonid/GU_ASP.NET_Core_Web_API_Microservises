﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.DAL
{
    public class DotNetMetrics
    {
        public int Id { get; set; }
        public long Time { get; set; }
        public int Value { get; set; }
    }
}
