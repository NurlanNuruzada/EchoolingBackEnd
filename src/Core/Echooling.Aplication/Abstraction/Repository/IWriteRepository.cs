using Ecooling.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echooling.Aplication.Abstraction.Repository
{
    public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity, new()
    {
        Task addAsync(T entity);
        Task addRangeAsync(ICollection<T> enitites);
        void update(T entity);
        void remove(T entity);
        void removeRange(ICollection<T> enitites);
        Task SaveChangesAsync();
    }
}
