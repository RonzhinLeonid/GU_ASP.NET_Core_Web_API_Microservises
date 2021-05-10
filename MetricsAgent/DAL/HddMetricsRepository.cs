using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using Microsoft.Extensions.Logging;

namespace MetricsAgent.DAL
{
    public class HddMetricsRepository : IHddMetricsRepository
    {
        private const string ConnectionString = SQLConnectionString.ConnectionString;
        private readonly ILogger<HddMetricsRepository> _logger;
        public HddMetricsRepository(ILogger<HddMetricsRepository> logger)
        {
            _logger = logger;
        }

        public void Create(HddMetrics item)
        {
            throw new NotImplementedException();
        }

        public IList<HddMetrics> GetByTimePeriod(DateTimeOffset from, DateTimeOffset to)
        {
            var fromSeconds = from.ToUnixTimeSeconds();
            var toSeconds = to.ToUnixTimeSeconds();
            if (fromSeconds > toSeconds) return null;

            using var connection = new SQLiteConnection(ConnectionString);
            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "SELECT * FROM hddmetrics WHERE (time >= @from) and (time <= @to)";
            cmd.Parameters.AddWithValue("@from", fromSeconds);
            cmd.Parameters.AddWithValue("@to", toSeconds);
            cmd.Prepare();

            connection.Open();
            var temp = new List<HddMetrics>();
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                temp.Add(new HddMetrics()
                {
                    Id = reader.GetInt32(0),
                    Value = reader.GetInt32(1),
                    Time = reader.GetInt32(2)
                });
            }
            connection.Close();
            return temp.Count > 0 ? temp : null;
        }
    }
}
