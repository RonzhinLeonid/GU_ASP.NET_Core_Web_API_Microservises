using System;
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
            using (var connection = new SQLiteConnection(SQLConnectionString.ConnectionString))
            {
                var result = connection.Execute(
                $"INSERT INTO cpumetrics(Time,Value) VALUES (@Time,@Value);",
                    new
                    {
                        Time = item.Time,
                        Value = item.Value,
                    }
                );
                if (result <= 0) throw new InvalidOperationException("Не удалось добавить метрику.");
            }
        }

        public IList<CpuMetric> GetByTimePeriod(DateTimeOffset from, DateTimeOffset to)
        {
            var fromSeconds = from.ToUnixTimeSeconds();
            var toSeconds = to.ToUnixTimeSeconds();
            if (fromSeconds > toSeconds) return new List<CpuMetric>();

            using (var connection = new SQLiteConnection(SQLConnectionString.ConnectionString))
            {
                var commandParameters =  new { from = fromSeconds, to = toSeconds };
                return connection.Query<CpuMetric>("SELECT * FROM cpumetrics WHERE (time >= @from) and (time <= @to)",
                                                    commandParameters).ToList();
            }
        }
    }
}
