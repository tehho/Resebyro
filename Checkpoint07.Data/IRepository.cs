using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkpoint07.Domain;

namespace Checkpoint07.Data
{
    public interface IRepository<T>
    {
        T Add(T obj);
        T Get(T obj);
        T Get(Func<T, bool> method);
        T Update(T obj);
        T Remove(T obj);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(Func<T, bool> method);
    }
}
