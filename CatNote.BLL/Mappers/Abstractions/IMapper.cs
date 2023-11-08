namespace CatNote.BLL.Mappers.Abstractions;

public interface IMapper<TEntity, TModel>
{
    public TEntity ToEntity(TModel destinationEntity);
    public TModel FromEntity(TEntity sourceEntity);
}