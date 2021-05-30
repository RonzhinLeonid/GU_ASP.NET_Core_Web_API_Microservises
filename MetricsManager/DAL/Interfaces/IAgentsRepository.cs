using MetricsManager.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.DAL.Interfaces
{
    public interface IAgentsRepository
    {
        void Create(AgentInfo agent);
        IList<AgentInfo> Get();
        AgentInfo GetById(int id);
        void Update(AgentInfo agent);
    }
}
