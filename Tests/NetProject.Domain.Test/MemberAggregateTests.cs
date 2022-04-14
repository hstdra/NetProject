using NetProject.Domain.MemberAggregate;
using Xunit;

namespace NetProject.Domain.Test;

public class MemberAggregateTests
{
    [Fact]
    public void GivenInformation_WhenCreatingMember_ThenItShouldBeCreated()
    {
        var name = "Test User";
        var username = "test";

        var member = new Member(name, username);

        Assert.Equal(name, member.Name);
        Assert.Equal(username, member.Username);
    }
}