using CatNote.DAL.Entities;
using CatNote.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CatNote.DAL.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    protected ApplicationDbContext dbContext { get; }
    protected DbSet<TEntity> dbSet { get; }

    public GenericRepository(ApplicationDbContext applicationDbContext)
    {
        this.dbContext = applicationDbContext;
        dbSet = applicationDbContext.Set<TEntity>();
    }

    public async Task Create(TEntity element)
    {
        dbSet.Add(element);
        await dbContext.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var element = dbSet.FirstOrDefault(x => x.Id == id);

        if (element != null)
        {
            dbSet.Remove(element);
            await dbContext.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<TEntity>> GetAll()
    {
        return await dbSet.AsNoTracking().ToListAsync();
    }

    public async Task<TEntity> GetById(int id)
    {
        var result = await dbSet.FindAsync(id);
        return result!;
    }

    public async Task Update(TEntity element)
    {
        dbContext.Entry(element).State = EntityState.Modified;
        await dbContext.SaveChangesAsync();
    }
}
