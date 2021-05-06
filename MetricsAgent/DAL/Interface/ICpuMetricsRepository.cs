using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.DAL
{
    /// <summary>
    /// Маркировочный интерфейс, необходим, чтобы проверить работу репозитория на тесте-заглушке
    /// </summary>
    public interface ICpuMetricsRepository : IRepository<CpuMetric>
    {

    }
}
