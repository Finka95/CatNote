namespace CatNote.API.Controllers;

public interface IGenericController<TDto>
{
    Task<TDto> Create(TDto dto, CancellationToken cancellationToken);
    Task Delete(int id, CancellationToken cancellationToken);
    Task<IEnumerable<TDto>> GetAll(CancellationToken cancellationToken);
    Task<TDto> GetById(int id, CancellationToken cancellationToken);
    Task<TDto> Update(TDto dto, CancellationToken cancellationToken);
}
