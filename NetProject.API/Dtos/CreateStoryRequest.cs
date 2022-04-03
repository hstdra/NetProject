namespace NetProject.API.Dtos;

public class CreateStoryRequest
{
    public string Name { get; init; }
    public Guid CreatorId { get; init; }
}