using Task.DTO;

namespace Task.Service.Interfaces.Repository;

public interface IUserRepository : IRepositoryBase<User>
{ 
    IList<User> GetRelations(RelationType relationType);
}