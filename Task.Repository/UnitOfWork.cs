using Task.Service.Interfaces.Repository;

namespace Task.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TaskDbContext _context;
        private readonly Lazy<IUserRepository> _userRepository;
        private readonly Lazy<IRelationRepository> _relationRepository;
        private readonly Lazy<ICityRepository> _cityRepository;
        private readonly Lazy<IPhoneNumberRepository> _phoneNumberRepository;

        public UnitOfWork(TaskDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _userRepository = new Lazy<IUserRepository>(() => new UserRepository(context));
            _cityRepository = new Lazy<ICityRepository>(() => new CityRepository(context));
            _relationRepository = new Lazy<IRelationRepository>(() => new RelationRepository(context));
            _phoneNumberRepository = new Lazy<IPhoneNumberRepository>(() => new PhoneNumberRepository(context));   
        }
        public IUserRepository UserRepository => _userRepository.Value;
        public ICityRepository CityRepository => _cityRepository.Value;
        public IRelationRepository RelationRepository => _relationRepository.Value;
        public IPhoneNumberRepository PhoneNumberRepository => _phoneNumberRepository.Value;

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
        public void BeginTransaction()
        {
            if (_context.Database.CurrentTransaction != null)
            {
                throw new InvalidOperationException("A Transaction is already in progress.");
            }

            _context.Database.BeginTransaction();
        }
        public void CommitTransaction()
        {
            try
            {
                _context.Database.CurrentTransaction?.Commit();
            }
            catch
            {
                _context.Database.CurrentTransaction?.Rollback();
                throw;
            }
            finally
            {
                _context.Database.CurrentTransaction?.Dispose();
            }
        }
        public void RollBack()
        {
            try
            {
                _context.Database.CurrentTransaction?.Rollback();
            }
            finally
            {
                _context.Database.CurrentTransaction?.Dispose();
            }
        }
        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}