﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using Microsoft.Extensions.Logging;
using Dapper;

namespace MetricsAgent.DAL
{
    public class DotNetMetricsRepository : IDotNetMetricsRepository
    {
        private readonly ILogger<DotNetMetricsRepository> _logger;
        public DotNetMetricsRepository(ILogger<DotNetMetricsRepository> logger)
        {
            _logger = logger;
        }

        public void Create(DotNetMetrics item)
        {
            using (var connection = new SQLiteConnection(SQLConnectionString.ConnectionString))
            {
                var result = connection.Execute(
                $"INSERT INTO dotnetmetrics(Time,Value) VALUES (@Time,@Value);",
                    new
                    {
                        Time = item.Time,
                        Value = item.Value,
                    }
                );
                if (result <= 0) throw new InvalidOperationException("Не удалось добавить метрику.");
            }
        }

        public IList<DotNetMetrics> GetByTimePeriod(DateTimeOffset from, DateTimeOffset to)
        {
            var fromSeconds = from.ToUnixTimeSeconds();
            var toSeconds = to.ToUnixTimeSeconds();
            if (fromSeconds > toSeconds) return null;

            using (var connection = new SQLiteConnection(SQLConnectionString.ConnectionString))
            {
                var commandParameters = new { from = fromSeconds, to = toSeconds };
                return connection.Query<DotNetMetrics>("SELECT * FROM dotnetmetrics WHERE (time >= @from) and (time <= @to)",
                                                    commandParameters).ToList();
            }
        }
    }
}
