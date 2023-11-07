namespace CatNote.Common.Interfaces;

public interface IMapper<TSourceEntity, TDestinationEntity>
{
    public TSourceEntity ToEntity(TDestinationEntity destinationEntity);
    public TDestinationEntity FromEntity(TSourceEntity sourceEntity);
}