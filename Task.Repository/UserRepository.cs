using Task.DTO;
using Task.Service.Interfaces.Repository;

namespace Task.Repository;

internal sealed class UserRepository : RepositoryBase<User>, IUserRepository
{
    internal UserRepository(TaskDbContext context) : base(context)
    {

    }

    public IList<User> GetRelations(RelationType relationType)
    {
        return _context.Relations
            .Where(r => r.RelationType == relationType)
            .Select(r => r.FromUser)
            .ToList();
    }
}