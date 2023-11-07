using CatNote.DAL.Entities;
using CatNote.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CatNote.DAL.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly ApplicationDbContext dbContext;
    protected readonly DbSet<TEntity> dbSet;

    public GenericRepository(ApplicationDbContext applicationDbContext)
    {
        this.dbContext = applicationDbContext;
        dbSet = applicationDbContext.Set<TEntity>();
    }

    public async Task<TEntity> Create(TEntity element, CancellationToken cancellationToken)
    {
        dbSet.Add(element);
        await dbContext.SaveChangesAsync(cancellationToken);

        return element;
    }

    public async Task Delete(int id, CancellationToken cancellationToken)
    {
        var element = await dbSet.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (element != null)
        {
            dbSet.Remove(element);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<IEnumerable<TEntity>> GetAll(CancellationToken cancellationToken)
    {
        return await dbSet.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<TEntity> GetById(int id, CancellationToken cancellationToken)
    {
        var result = await dbSet.FindAsync(id, cancellationToken);
        return result!;
    }

    public async Task<TEntity> Update(TEntity element, CancellationToken cancellationToken)
    {
        dbContext.Entry(element).State = EntityState.Modified;
        await dbContext.SaveChangesAsync(cancellationToken);

        return element;
    }
}
