using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatNote.DAL.Entities.Abstractions;

namespace CatNote.BLL.Services;

public interface IGenericService<TModel>
{
    Task<TModel> Create(TModel element, CancellationToken cancellationToken);
    Task Delete(int id, CancellationToken cancellationToken);
    Task<IEnumerable<TModel>> GetAll(CancellationToken cancellationToken);
    Task<TModel> Update(TModel element, CancellationToken cancellationToken);
    Task<TModel> GetById(int id, CancellationToken cancellationToken);
}