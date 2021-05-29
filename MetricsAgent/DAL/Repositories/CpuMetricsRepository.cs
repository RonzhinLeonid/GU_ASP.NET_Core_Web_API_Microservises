﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Dapper;

namespace MetricsAgent.DAL
{
    public class CpuMetricsRepository : ICpuMetricsRepository
    {
        private readonly ILogger<CpuMetricsRepository> _logger;
        public CpuMetricsRepository(ILogger<CpuMetricsRepository> logger)
        {
            _logger = logger;
        }

        public void Create(CpuMetric item)
        {
            throw new NotImplementedException();
        }

        public IList<CpuMetric> GetByTimePeriod(DateTimeOffset from, DateTimeOffset to)
        {
            var fromSeconds = from.ToUnixTimeSeconds();
            var toSeconds = to.ToUnixTimeSeconds();
            if (fromSeconds > toSeconds) return null;

            using (var connection = new SQLiteConnection(SQLConnectionString.ConnectionString))
            {
                var commandParameters =  new { from = fromSeconds, to = toSeconds };
                return connection.Query<CpuMetric>("SELECT * FROM cpumetrics WHERE (time >= @from) and (time <= @to)",
                                                    commandParameters).ToList();
            }
        }
    }
}
