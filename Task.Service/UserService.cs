using Task.DTO;
using Task.Service.Interfaces.Repository;
using Task.Service.Interfaces.Services;

namespace Task.Service;

public sealed class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public void CreateUser(User user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));
        _unitOfWork.UserRepository.Insert(user);
        SaveChanges();
    }

    public User GetUser(int userId)
    {
        User user = _unitOfWork.UserRepository.Get(userId);
        if (user == null) throw new InvalidDataException("The UserId could not be found");

        return user;
    }

    public IQueryable<User> GetUsers()
    {
        var users = _unitOfWork.UserRepository.GetAll();
        if (users == null) throw new InvalidDataException("The UserId could not be found");

        return users;
    }

    public void UpdateUser(User user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));
        var currentUser = _unitOfWork.UserRepository.GetAll().SingleOrDefault(u => u.Id == user.Id);

        if (currentUser == null) throw new InvalidDataException("User not found");

        _unitOfWork.UserRepository.Update(currentUser);
        SaveChanges();
    }

    public void DeleteUser(int id)
    {
        User user = _unitOfWork.UserRepository.Get(id) ?? throw new ArgumentNullException($"The user with Id: {id} does not exist.");

        user.IsDeleted = true;
        _unitOfWork.UserRepository.Update(user);
        SaveChanges();
    }

    public void AddRelation(User user, User relatedUser, RelationType relationType)
    {
        Relation relation = new() { FromUser = user, ToUser = relatedUser, RelationType = relationType };

        user.RelationsFrom?.Add(relation);
        relatedUser.RelationsTo?.Add(relation);

        _unitOfWork.UserRepository.Update(user);
        _unitOfWork.UserRepository.Update(relatedUser);
        SaveChanges();
    }

    public void RemoveRelation(int relationId)
    {
        Relation relation = _unitOfWork.RelationRepository.Get(relationId) ?? 
            throw new ArgumentNullException($"The relation with Id: {relationId} does not exist");

        relation.IsDeleted = true;
        _unitOfWork.RelationRepository.Update(relation);
        SaveChanges();
    }

    public void SaveChanges()
    {
        _unitOfWork.SaveChanges();
    }
}