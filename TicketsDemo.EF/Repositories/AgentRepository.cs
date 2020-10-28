using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using TicketsDemo.Data.Repositories;
namespace TicketsDemo.EF.Repositories
{
    public class AgentRepository: IAgentRepository
    {
        public void CreateAgent(Agent agent)
        {
            using (var ctx = new TicketsContext())
            {
                ctx.Agents.Add(agent);
                ctx.SaveChanges();
            }
        }
        public void DeleteAgent(string agentId)
        {
            using (var ctx = new TicketsContext())
            {
                var agent = ctx.Agents.Find(agentId);            
                ctx.Agents.Remove(agent);
                ctx.SaveChanges();
            }
        }
        public List<Agent> GetAgents()
        {
            using (var ctx = new TicketsContext())
            {                
                return ctx.Agents.ToList();
            }
        }
        public Agent GetAgent(string agentId ="0")
        {
            using (var ctx = new TicketsContext())
            {
                return ctx.Agents.Where(p => p.Id == agentId).FirstOrDefault();
            }
        }
    }
}
