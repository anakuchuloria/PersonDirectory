using Task.DTO;

namespace Task.Service.Interfaces.Services;

public interface IUserService
{
    Task<User> GetUser(int id);
    Task<IQueryable<User>> GetUsers();
    void CreateUser(User user);
    void UpdateUser(User user);
    void DeleteUser(int userId);
    void AddRelation(User user, User relatedUser, RelationType relationType);
    void RemoveRelation(int relationId);
}