using NetProject.Domain.MemberAggregate;
using NetProject.Infrastructure.Cqrs.Queries;

namespace NetProject.Application.Queries;

public record GetMemberByIdQuery(Guid Id) : IQuery<Member>;

public class GetMemberByIdQueryHandler : IQueryHandler<GetMemberByIdQuery, Member>
{
    private readonly IMemberRepository _memberRepository;

    public GetMemberByIdQueryHandler(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }

    public Task<Member> Handle(GetMemberByIdQuery request, CancellationToken cancellationToken)
    {
        return _memberRepository.FindOneAsync(request.Id, cancellationToken);
    }
}