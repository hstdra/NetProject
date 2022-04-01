using NetProject.Domain.MemberAggregate;
using NetProject.Infrastructure.Database;

namespace NetProject.Infrastructure.Domain.Repositories;

public class MemberRepository : RepositoryBase<Member, Guid>, IMemberRepository
{
    public MemberRepository(AppDbContext context) : base(context)
    {
    }
}