using CatNote.BLL.Exceptions;
using CatNote.BLL.Interfaces;
using CatNote.BLL.Mappers.Abstractions;
using CatNote.DAL.Entities.Abstractions;
using CatNote.DAL.Interfaces;

namespace CatNote.BLL.Services;

public class GenericService<TModel, TEntity> : IGenericService<TModel>
    where TEntity : IBaseEntity
{
    private readonly IMapper<TEntity, TModel> _mapper;
    private readonly IGenericRepository<TEntity> _genericRepository;

    public GenericService(IMapper<TEntity, TModel> mapper, IGenericRepository<TEntity> genericRepository)
    {
        _mapper = mapper;
        _genericRepository = genericRepository;
    }

    public async Task<TModel> Create(TModel model, CancellationToken cancellationToken)
    {
        var entity = _mapper.ToEntity(model);

        var resultEntity = await _genericRepository.Create(entity, cancellationToken);

        return _mapper.FromEntity(resultEntity);
    }

    public async Task Delete(int id, CancellationToken cancellationToken) => await _genericRepository.Delete(id, cancellationToken);

    public async Task<IEnumerable<TModel>> GetAll(CancellationToken cancellationToken)
    {
        var resultEntity = await _genericRepository.GetAll(cancellationToken);

        return resultEntity.Select(x => _mapper.FromEntity(x));
    }

    public async Task<TModel> Update(TModel model, CancellationToken cancellationToken)
    {
        var entity = _mapper.ToEntity(model);

        var resultEntity = await _genericRepository.Update(entity, cancellationToken);

        return resultEntity == null
            ? throw new NotFoundException("Not found")
            : _mapper.FromEntity(resultEntity);
    }

    public async Task<TModel> GetById(int id, CancellationToken cancellationToken)
    {
        var resultEntity = await _genericRepository.GetById(id, cancellationToken);

        return resultEntity == null
            ? throw new NotFoundException("Not found")
            : _mapper.FromEntity(resultEntity);
    }
}