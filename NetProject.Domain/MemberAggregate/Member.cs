using NetProject.Domain.Core;

namespace NetProject.Domain.MemberAggregate;

public class Member : AggregateRoot<Guid>
{
    public string Name { get; }
    public string Username { get; }
    
    public Member(string name, string username)
    {
        Name = name;
        Username = username;
    }
}