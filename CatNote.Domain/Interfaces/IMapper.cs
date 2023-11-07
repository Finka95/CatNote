namespace CatNote.Domain.Interfaces;

public interface IMapper<T1, T2>
{
    public T1 ToEntity(T2 destinationEntity);
    public T2 FromEntity(T1 sourceEntity);
}