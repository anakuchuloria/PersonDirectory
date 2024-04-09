using Task.DTO;
using Task.Service.Interfaces.Repository;

namespace Task.Repository;

internal sealed class RelationRepository : RepositoryBase<Relation>, IRelationRepository
{
    public RelationRepository(TaskDbContext context) : base(context)
    {

    }
}