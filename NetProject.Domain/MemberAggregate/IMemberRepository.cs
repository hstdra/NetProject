using NetProject.Domain.Core;

namespace NetProject.Domain.MemberAggregate;

public interface IMemberRepository : IRepository<Member, Guid>
{

}