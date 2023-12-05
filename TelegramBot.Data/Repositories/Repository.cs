using TelegramBot.Domain.Commons;
using TelegramBot.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using TelegramBot.Data.IRepositories;

namespace TelegramBot.Data.Repositories;
public class Repository<Tentity> : IRepository<Tentity> where Tentity : Auditable
{
    private readonly DataContext _context;
    private readonly DbSet<Tentity> _set;

    public Repository(DataContext context)
    {
        _context = context;
        _set= context.Set<Tentity>();
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var res= await _set.FirstOrDefaultAsync(x => x.Id == id);
        _set.Remove(res);
        await _context.SaveChangesAsync();

        return true;
    }

    public IQueryable<Tentity> SelectAll()
    => _set;

    public async Task<Tentity> SelectByIdAsync(long id)
    => await _set.FirstOrDefaultAsync(x => x.Id == id);

    public async Task<Tentity> InsertAsync(Tentity entity)
    {
        var entry = await _set.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entry.Entity;
    }

    public async Task<Tentity> UpdateAsync(Tentity entity)
    {
        var entry =  _set.Update(entity);
        await _context.SaveChangesAsync();

        return entry.Entity;
    }
}
