using TelegramBot.Domain.Commons;

namespace TelegramBot.Data.IRepositories;
public interface IRepository<TEntity> where TEntity : Auditable
{
    Task<TEntity> InsertAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task<bool> DeleteAsync(long id);
    Task<TEntity> SelectByIdAsync(long id);
     IQueryable<TEntity> SelectAll();
}
