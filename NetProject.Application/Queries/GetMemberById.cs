using NetProject.Domain.Core;
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
        var specification = new SpecificationBase<Member>(x => request.Id == x.Id);
        return _memberRepository.FindOneAsync(specification, cancellationToken);
    }
}