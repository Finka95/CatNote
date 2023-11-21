using AutoMapper;
using CatNote.BLL.Interfaces;
using CatNote.DAL.Entities.Abstractions;
using CatNote.DAL.Interfaces;

namespace CatNote.BLL.Services;

public class GenericService<TModel, TEntity> : IGenericService<TModel>
    where TEntity : IBaseEntity
{
    private readonly IMapper _mapper;
    private readonly IGenericRepository<TEntity> _genericRepository;

    public GenericService(IMapper mapper, IGenericRepository<TEntity> genericRepository)
    {
        _mapper = mapper;
        _genericRepository = genericRepository;
    }

    public virtual async Task<TModel> Create(TModel model, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<TEntity>(model);

        var resultEntity = await _genericRepository.Create(entity, cancellationToken);

        return _mapper.Map<TModel>(resultEntity);
    }

    public async Task Delete(int id, CancellationToken cancellationToken) => await _genericRepository.Delete(id, cancellationToken);

    public async Task<IEnumerable<TModel>> GetAll(CancellationToken cancellationToken)
    {
        var resultEntity = await _genericRepository.GetAll(cancellationToken);

        return _mapper.Map<List<TModel>>(resultEntity);
    }

    public async Task<TModel> Update(TModel model, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<TEntity>(model);

        var resultEntity = await _genericRepository.Update(entity, cancellationToken);

        return _mapper.Map<TModel>(resultEntity);
    }

    public async Task<TModel> GetById(int id, CancellationToken cancellationToken)
    {
        var resultEntity = await _genericRepository.GetById(id, cancellationToken);

        return _mapper.Map<TModel>(resultEntity);
    }
}