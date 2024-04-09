using Task.DTO;
using Task.Service.Interfaces.Repository;

namespace Task.Service.Interfaces.Services;

public interface IUserService
{
    User GetUser(int id);
    IQueryable<User> GetUsers();
    void CreateUser(User user);
    void UpdateUser(User user);
    void DeleteUser(int userId);
    void AddRelation(User user, User relatedUser, RelationType relationType);
    void RemoveRelation(int relationId);
}