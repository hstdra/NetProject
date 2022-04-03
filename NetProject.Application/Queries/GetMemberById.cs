using NetProject.Domain.MemberAggregate;
using NetProject.Infrastructure.Cqrs.Queries;

namespace NetProject.Application.Queries;

public class GetMemberByIdQuery : IQuery<Member>
{
    public Guid Id { get; init; }
}

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