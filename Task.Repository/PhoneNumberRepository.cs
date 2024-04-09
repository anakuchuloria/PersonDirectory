using Task.DTO;
using Task.Service.Interfaces.Repository;

namespace Task.Repository;
internal sealed class PhoneNumberRepository : RepositoryBase<PhoneNumber>, IPhoneNumberRepository
{
    public PhoneNumberRepository(TaskDbContext context) : base(context)
    {

    }
}