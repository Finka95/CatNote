namespace CatNote.API.Controllers;

public interface IGenericController<TDTO>
{
    Task<TDTO> Create(TDTO dto, CancellationToken cancellationToken);
    Task Delete(int id, CancellationToken cancellationToken);
    Task<IEnumerable<TDTO>> GetAll(CancellationToken cancellationToken);
    Task<TDTO> GetById(int id, CancellationToken cancellationToken);
    Task<TDTO> Update(TDTO dto, CancellationToken cancellationToken);
}