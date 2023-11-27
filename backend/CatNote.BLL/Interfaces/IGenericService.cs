namespace CatNote.BLL.Interfaces;

public interface IGenericService<TModel>
{
    Task<TModel> Create(TModel element, CancellationToken cancellationToken);
    Task Delete(int id, CancellationToken cancellationToken);
    Task<IEnumerable<TModel>> GetAll(CancellationToken cancellationToken);
    Task<TModel> Update(TModel element, CancellationToken cancellationToken);
    Task<TModel> GetById(int id, CancellationToken cancellationToken);
}