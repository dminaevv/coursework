using System.Linq.Expressions;

namespace EFConfigurator.Repositories;

public interface IDbRepository
{
    void SaveChanges();
    Task<Int32> SaveChangesAsync();

    Task Add<T>(T newEntity) where T : class;
    Task AddRange<T>(IEnumerable<T> newEntities) where T : class;

    IQueryable<T> Get<T>(Expression<Func<T, Boolean>> selector) where T : class;
    IQueryable<T> Get<T>() where T : class;
    T? FirstOrDefault<T>(Expression<Func<T, bool>> selector) where T : class;
    T First<T>(Expression<Func<T, bool>> selector) where T : class;
    IQueryable<T> GetAll<T>() where T : class;

    Task Update<T>(T entity) where T : class;
    Task UpdateRange<T>(IEnumerable<T> entities) where T : class;
}
