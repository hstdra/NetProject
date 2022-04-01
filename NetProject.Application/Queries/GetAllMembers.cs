using NetProject.Domain.Core;
using NetProject.Domain.MemberAggregate;
using NetProject.Infrastructure.Cqrs.Queries;

namespace NetProject.Application.Queries;

public record GetAllMembersQuery : IQuery<IEnumerable<Member>>;

public class GetAllMembersQueryHandler : IQueryHandler<GetAllMembersQuery, IEnumerable<Member>>
{
    private readonly IMemberRepository _memberRepository;

    public GetAllMembersQueryHandler(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }

    public Task<IEnumerable<Member>> Handle(GetAllMembersQuery request, CancellationToken cancellationToken)
    {
        var specification = new SpecificationBase<Member>(x => true);
        return _memberRepository.FindAllAsync(specification, cancellationToken);
    }
}