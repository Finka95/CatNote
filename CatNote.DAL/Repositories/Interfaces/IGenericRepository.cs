using CatNote.DAL.Entities.Abstractions;

namespace CatNote.DAL.Repositories.Interfaces;

public interface IGenericRepository<TEntity> where TEntity : IBaseEntity
{
    Task Create(TEntity element);
    Task Delete(int id, CancellationToken cancellationToken);
    Task<IEnumerable<TEntity>> GetAll();
    Task Update(TEntity element);
    Task<TEntity> GetById(int id);
}
