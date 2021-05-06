using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using Microsoft.Extensions.Logging;
using Dapper;

namespace MetricsAgent.DAL
{
    public class RamMetricsRepository : IRamMetricsRepository
    {
        private const string ConnectionString = SQLConnectionString.ConnectionString;
        private readonly ILogger<RamMetricsRepository> _logger;
        public RamMetricsRepository(ILogger<RamMetricsRepository> logger)
        {
            _logger = logger;
        }

        public void Create(RamMetrics item)
        {
            throw new NotImplementedException();
        }

        public IList<RamMetrics> GetByTimePeriod(DateTimeOffset from, DateTimeOffset to)
        {
            var fromSeconds = from.ToUnixTimeSeconds();
            var toSeconds = to.ToUnixTimeSeconds();
            if (fromSeconds > toSeconds) return null;

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                var commandParameters = new { from = fromSeconds, to = toSeconds };
                return connection.Query<RamMetrics>("SELECT * FROM rammetrics WHERE (time >= @from) and (time <= @to)",
                                                    commandParameters).ToList();
            }
        }
    }
}
