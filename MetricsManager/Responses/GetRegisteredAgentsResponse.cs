﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Responses
{
    public class GetRegisteredAgentsResponse
    {
        public IEnumerable<AgentsResponse> Agents { get; set; }
    }
}
