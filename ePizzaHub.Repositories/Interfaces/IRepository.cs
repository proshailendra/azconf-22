using System.Collections.Generic;

namespace ePizzaHub.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();

        TEntity Find(object Id);

        void Add(TEntity entity);

        void Update(TEntity entity);

        void Remove(TEntity entity);

        void Delete(object Id);

        int SaveChanges();
    }
}