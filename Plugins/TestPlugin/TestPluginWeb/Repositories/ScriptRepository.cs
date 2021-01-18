using CommonInfrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestData.Models;
using TestPluginWeb.Interfaces;

namespace TestPluginWeb.Repositories
{
    public class ScriptRepository : IRepository<ScriptP>
    {
        List<ScriptP> scripts { get; set; } = new List<ScriptP>
        {
                new ScriptP { Id = 0, Name = "TestScript1", Duration=1500 },
                new ScriptP { Id = 1, Name = "TestScript1", Duration=0 },
                new ScriptP { Id = 2, Name = "TestScript3", Duration=4000 },
                new ScriptP { Id = 3, Name = "TestScript4", Duration=1000 },
                new ScriptP { Id = 4, Name = "TestScript5", Duration=2000 }
        };

        public List<ScriptP> GetAll() => new List<ScriptP>(scripts);
        public ScriptP Get(int id)
        {
            try
            {
                return (from script in scripts where script.Id == id select script).First();
            }
            catch(Exception ex)
            {
                throw new Exception($"Script with id:{id} was not found in ScriptRepository", ex);
            }
        }

        public void Add(ScriptP item)
        {
            scripts.Add(item);
        }
    }
}
