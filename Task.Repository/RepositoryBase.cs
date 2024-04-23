using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Task.Service.Interfaces.Repository;

namespace Task.Repository
{
    internal abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly TaskDbContext _context;
        protected readonly DbSet<T> _dbSet;

        internal RepositoryBase(TaskDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<T>();
        }
        public T Get(params object[] id) =>
           _dbSet.Find(id) ?? throw new KeyNotFoundException($"Record with key {id} not found");

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate) =>
            _dbSet.Where(predicate);

        public IQueryable<T> GetAll() =>
            _dbSet;

        public void Insert(T entity) =>
            _dbSet.Add(entity);

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
        public void InsertOrUpdate(T entity)
        {
            var entry = _context.Entry(entity);
            if (entry == null || entry.State == EntityState.Detached)
            {
                Insert(entity);
            }
            else
            {
                Update(entity);
            }
        }
        public void Delete(object id) =>
            Delete(Get(id));

        public void Delete(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
        }

        public async Task<T> GetAsync(params object[] id) => await _dbSet.FindAsync(id) ?? throw new KeyNotFoundException();

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default) =>
           await _dbSet.ToListAsync(cancellationToken);

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default) =>
            await _dbSet.Where(expression).ToListAsync(cancellationToken);
    }
}