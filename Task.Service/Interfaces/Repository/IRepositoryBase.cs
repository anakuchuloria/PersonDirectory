using System.Linq.Expressions;

namespace Task.Service.Interfaces.Repository;

public interface IRepositoryBase<T> where T : class
{
    T Get(params object[] id);
    IQueryable<T> GetAll(Expression<Func<T, bool>> predicate);
    IQueryable<T> GetAll();
    void Insert(T entity);
    void Update(T entity);
    void InsertOrUpdate(T entity);
    void Delete(object id);
    void Delete(T entity);

    Task<T> GetAsync(params object[] id);

    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);
}