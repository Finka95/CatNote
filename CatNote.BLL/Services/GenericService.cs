using CatNote.BLL.Mappers.Abstractions;
using CatNote.DAL.Entities.Abstractions;
using CatNote.DAL.Repositories.Interfaces;

namespace CatNote.BLL.Services;

public class GenericService<TModel, TEntity> : IGenericService<TModel, TEntity>
    where TEntity : IBaseEntity
{
    private readonly IMapper<TEntity, TModel> _mapper;
    private readonly IGenericRepository<TEntity> _genericRepository;

    public GenericService(IMapper<TEntity, TModel> mapper, IGenericRepository<TEntity> genericRepository)
    {
        _mapper = mapper;
        _genericRepository = genericRepository;
    }

    public async Task Create(TModel model, CancellationToken cancellationToken)
    {
        var entity = _mapper.ToEntity(model);

        await _genericRepository.Create(entity, cancellationToken);
    }

    public async Task Delete(int id, CancellationToken cancellationToken)
    {
        await _genericRepository.Delete(id, cancellationToken);
    }

    public async Task<IEnumerable<TModel>> GetAll(CancellationToken cancellationToken)
    {
        var resultEntity = await _genericRepository.GetAll(cancellationToken);

        return resultEntity.Select(x => _mapper.FromEntity(x));
    }

    public async Task Update(TModel model, CancellationToken cancellationToken)
    {
        var entity = _mapper.ToEntity(model);

        await _genericRepository.Update(entity, cancellationToken);
    }

    public async Task<TModel> GetById(int id, CancellationToken cancellationToken)
    {
        var resultEntity = await _genericRepository.GetById(id, cancellationToken);

        return _mapper.FromEntity(resultEntity);
    }
}