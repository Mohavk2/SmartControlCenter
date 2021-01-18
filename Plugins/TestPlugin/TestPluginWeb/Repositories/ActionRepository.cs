using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestData.Models;
using TestPluginWeb.Interfaces;

namespace TestPluginWeb.Repositories
{
    public class ActionRepository : IRepository<ActionP>
    {
        static int count = 0;
        List<ActionP> actions { get; set; } = new List<ActionP>();
        public List<ActionP> GetAll() => new List<ActionP>(actions);
        public ActionP Get(int id)
        {
            try
            {
                return (from action in actions where action.Id == id select action).First();
            }
            catch (Exception ex)
            {
                throw new Exception($"Action with id:{id} was not found in ScriptRepository", ex);
            }
        }

        public void Add(ActionP item)
        {
            item.Id = count++;
            actions.Add(item);
        }
    }
}
