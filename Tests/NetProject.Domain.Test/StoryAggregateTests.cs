using System;
using NetProject.Domain.StoryAggregate;
using Xunit;

namespace NetProject.Domain.Test;

public class StoryAggregateTests
{
    [Fact]
    public void GivenInformation_WhenCreatingStory_ThenItShouldBeCreated()
    {
        var name = "Test Story";
        var creatorId = Guid.NewGuid();

        var story = new Story(name, creatorId);
        
        Assert.Equal(name, story.Name);
        Assert.Equal(creatorId, story.CreatorId);
        Assert.Empty(story.OwnerIds);
        Assert.Empty(story.StoryTasks);
    }
    
    [Fact]
    public void GivenAStory_WhenAddingOwners_ThenItShouldBeAdded()
    {
        var ownerId1 = Guid.NewGuid();
        var ownerId2 = Guid.NewGuid();
        var story = GivenSampleStory();

        story.AddOwner(ownerId1);
        story.AddOwner(ownerId2);
        
        Assert.Equal(2, story.OwnerIds.Count);
        Assert.Equal(ownerId1, story.OwnerIds[0]);
        Assert.Equal(ownerId2, story.OwnerIds[1]);
    }
    
    [Fact]
    public void GivenAStoryWithOwner_WhenRemovingOwner_ThenItShouldBeRemoved()
    {
        var ownerId = Guid.NewGuid();
        var story = GivenSampleStory();
        
        story.AddOwner(ownerId);
        story.RemoveOwner(ownerId);
        
        Assert.Empty(story.OwnerIds);
    }
    
    [Fact]
    public void GivenAStory_WhenAddingTask_ThenItShouldBeAdded()
    {
        var taskName1 = "Task 1";
        var taskName2 = "Task 2";
        var story = GivenSampleStory();
        
        story.AddStoryTask(taskName1);
        story.AddStoryTask(taskName2);
        
        Assert.Equal(2, story.StoryTasks.Count);
        Assert.Equal(taskName1, story.StoryTasks[0].Name);
        Assert.Equal(taskName2, story.StoryTasks[1].Name);
    }
    
    [Fact]
    public void GivenAStoryWithTask_WhenRemovingTask_ThenItShouldBeRemoved()
    {
        var taskName = "Task 1";
        var story = GivenSampleStory();
        
        story.AddStoryTask(taskName);
        story.RemoveStoryTask(story.StoryTasks[0].StoryId);
        
        Assert.Empty(story.StoryTasks);
    }
    
    [Fact]
    public void GivenAStory_WhenUpdatingTask_ThenItShouldBeUpdated()
    {
        var taskName = "Task 1";
        var story = GivenSampleStory();
        
        story.AddStoryTask(taskName);
        story.ChangeStoryTaskIsDone(story.StoryTasks[0].StoryId, true);
        
        Assert.True(story.StoryTasks[0].IsDone);
    }

    private Story GivenSampleStory()
    {
        var name = "Test Story";
        var creatorId = Guid.NewGuid();

        return new Story(name, creatorId);
    }
}