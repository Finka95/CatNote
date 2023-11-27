using CatNote.DAL.Entities.Abstractions;

namespace CatNote.DAL.Entities;

public class BaseEntity : IBaseEntity
{
    public int Id { get; set; }
}
