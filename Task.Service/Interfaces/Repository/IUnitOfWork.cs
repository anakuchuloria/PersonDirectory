namespace Task.Service.Interfaces.Repository;

public interface IUnitOfWork : IDisposable
{
    IUserRepository UserRepository { get; }
    ICityRepository CityRepository { get; }
    IRelationRepository RelationRepository { get; }
    int SaveChanges();
    void BeginTransaction();
    void CommitTransaction();
    void RollBack();
}