using NetProject.Domain.MemberAggregate;
using NetProject.Infrastructure.Cqrs.Commands;

namespace NetProject.Application.Commands;

public class CreateMemberCommand : ICommand<Guid>
{
    public string Username { get; init; }
    public string Name { get; init; }
}

public class CreateMemberCommandHandler : ICommandHandler<CreateMemberCommand, Guid>
{
    private readonly IMemberRepository _memberRepository;

    public CreateMemberCommandHandler(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }

    public async Task<CommandResult<Guid>> Handle(CreateMemberCommand command, CancellationToken cancellationToken)
    {
        var member = new Member(command.Name, command.Username);
        await _memberRepository.AddAsync(member, cancellationToken);

        return CommandResult<Guid>.Success(member.Id);
    }
}