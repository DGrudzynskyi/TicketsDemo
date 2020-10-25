using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;

namespace TicketsDemo.Data.Repositories
{
    public interface IAgentRepository
    {
        Agent GetAgent(string agentId);
        decimal AgentPercent(string agentId);
        List<Agent> GetAgents();
        void CreateAgent(Agent agent);
        void DeleteAgent(string agentId);
    }
}
