using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IList<T> GetByTimePeriod(DateTimeOffset from, DateTimeOffset to);
        IList<T> GetByTimePeriod(DateTimeOffset from, DateTimeOffset to, int agentId);

        void Create(T item);
        DateTimeOffset GetAgentLastMetricDate(int agentId);
    }
}
