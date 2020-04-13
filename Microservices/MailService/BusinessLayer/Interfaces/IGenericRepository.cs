using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCLDreamTeam.Mail.Domain.Interfaces
{
    public interface IGenericRepository<TEntity>
        where TEntity : class, IEntity
    {
    IQueryable<TEntity> GetAll();

    Task<TEntity> GetById(Guid id);

    Task Create(TEntity entity);

    Task Update(Guid id, TEntity entity);

    Task Delete(Guid id);
    }
}
