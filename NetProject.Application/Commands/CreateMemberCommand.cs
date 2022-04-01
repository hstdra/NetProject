using NetProject.Domain.MemberAggregate;
using NetProject.Infrastructure.Cqrs.Commands;

namespace NetProject.Application.Commands;

public record CreateMemberCommand(string Username, string Name) : ICommand<Guid>;

public class CreateMemberCommandHandler : ICommandHandler<CreateMemberCommand, Guid>
{
    private readonly IMemberRepository _memberRepository;

    public CreateMemberCommandHandler(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }

    public async Task<Guid> Handle(CreateMemberCommand command, CancellationToken cancellationToken)
    {
        var member = new Member(command.Name, command.Username);
        await _memberRepository.AddAsync(member, cancellationToken);

        return member.Id;
    }
}