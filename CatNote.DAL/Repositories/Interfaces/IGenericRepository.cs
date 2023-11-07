﻿using CatNote.DAL.Entities.Abstractions;

namespace CatNote.DAL.Repositories.Interfaces;

public interface IGenericRepository<TEntity> where TEntity : IBaseEntity
{
    Task Create(TEntity element, CancellationToken cancellationToken);
    Task Delete(int id, CancellationToken cancellationToken);
    Task<IEnumerable<TEntity>> GetAll(CancellationToken cancellationToken);
    Task Update(TEntity element, CancellationToken cancellationToken);
    Task<TEntity> GetById(int id, CancellationToken cancellationToken);
}
