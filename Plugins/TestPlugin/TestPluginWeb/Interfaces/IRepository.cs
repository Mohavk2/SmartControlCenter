using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestData.Models;

namespace TestPluginWeb.Interfaces
{
    public interface IRepository <T>
    {
        List<T> GetAll();
        T Get(int id);
        void Add(T item);
    }
}
