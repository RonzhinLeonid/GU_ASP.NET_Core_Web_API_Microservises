using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Data.SQLite;

namespace MetricsManager.DAL.Repositories
{
    public class HddMetricsRepository : IHddMetricsRepository
    {
        private readonly ILogger<HddMetricsRepository> _logger;
        public HddMetricsRepository(ILogger<HddMetricsRepository> logger)
        {
            _logger = logger;
        }
        public void Create(HddMetric item)
        {
            using (var connection = new SQLiteConnection(SQLConnectionString.ConnectionString))
            {
                var result = connection.Execute(
                $"INSERT INTO hddmetrics (agentId,time,value) VALUES (@agentId,@time,@value);",
                    new
                    {
                        AgentId = item.AgentId,
                        Time = item.Time,
                        Value = item.Value,
                    }
                );
                if (result <= 0) throw new InvalidOperationException("Не удалось добавить метрику.");
            }
        }

        public DateTimeOffset GetAgentLastMetricDate(int agentId)
        {
            using (var connection = new SQLiteConnection(SQLConnectionString.ConnectionString))
            {
                //var commandParameters = new { from = fromSeconds, to = toSeconds };
                var result = connection.ExecuteScalar<long>("SELECT Max(time) FROM hddmetrics WHERE agentId = @agentId",
                                                     new { agentId });
                if (result >= 0)
                {
                    return DateTimeOffset.FromUnixTimeSeconds(result);
                }
                throw new InvalidOperationException("Не удалось получить дату последней метрики");
            }
        }

        public IList<HddMetric> GetByTimePeriod(DateTimeOffset from, DateTimeOffset to)
        {
            var fromSeconds = from.ToUnixTimeSeconds();
            var toSeconds = to.ToUnixTimeSeconds();
            if (fromSeconds > toSeconds) return new List<HddMetric>();

            using (var connection = new SQLiteConnection(SQLConnectionString.ConnectionString))
            {
                var commandParameters = new { from = fromSeconds, to = toSeconds };
                return connection.Query<HddMetric>("SELECT * FROM hddmetrics WHERE (time >= @from) and (time <= @to)",
                                                    commandParameters).ToList();
            }
        }

        public IList<HddMetric> GetByTimePeriod(DateTimeOffset from, DateTimeOffset to, int agentId)
        {
            var fromSeconds = from.ToUnixTimeSeconds();
            var toSeconds = to.ToUnixTimeSeconds();
            if (fromSeconds > toSeconds) return new List<HddMetric>();

            using (var connection = new SQLiteConnection(SQLConnectionString.ConnectionString))
            {
                var commandParameters = new { from = fromSeconds, to = toSeconds };
                return connection.Query<HddMetric>("SELECT * FROM hddmetrics WHERE (agentId = @agentId) (time >= @from) and (time <= @to)",
                                                    commandParameters).ToList();
            }
        }
    }
}
