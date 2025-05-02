using AutoMapper;
using Contracts.Models;
using DataAccess.Entities;
using DataAccess.Profiles;
using TestSupport.Helpers;

namespace TaskManager.API.Tests.Profiles;

[TestClass]
public class WorkspaceProfileTests
{
    private IMapper _mapper = null!;
    
    [TestInitialize]
    public void Setup()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<TaskProfile>();
            cfg.AddProfile<WorkspaceProfile>();
        });

        _mapper = config.CreateMapper();
    }
    
    [TestMethod]
    public void WorkspaceProfile_GivenWorkspaceEntity_MapsToWorkspaceWithoutTasksModelCorrectly()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        string createdByUserEmail = "test@mail.com";
        WorkspaceEntity workspaceEntity = EntityHelper.GetDefaultWorkspaceEntity(id, createdByUserEmail);

        // Act
        WorkspaceWithoutTasks workspace = _mapper.Map<WorkspaceWithoutTasks>(workspaceEntity);

        // Assert
        Assert.AreEqual(workspaceEntity.Id, workspace.Id);
        Assert.AreEqual(workspaceEntity.CreatedAt, workspace.CreatedAt);
        Assert.AreEqual(workspaceEntity.UpdatedAt, workspace.UpdatedAt);
        Assert.AreEqual(workspaceEntity.Name, workspace.Name);
        Assert.AreEqual(workspaceEntity.FkCreatedByUserEmail, workspace.CreatedByUserEmail);
    }
    
    [TestMethod]
    public void WorkspaceProfile_GivenWorkspaceWithoutTasksModel_MapsToEntityCorrectly()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        WorkspaceWithoutTasks workspace = ModelHelper.GetDefaultWorkspaceWithoutTasksModel(id);
        
        // Act
        WorkspaceEntity workspaceEntity = _mapper.Map<WorkspaceEntity>(workspace);
        
        // Assert
        Assert.AreEqual(workspace.Id, workspaceEntity.Id);
        Assert.AreEqual(workspace.CreatedAt, workspaceEntity.CreatedAt);
        Assert.AreEqual(workspace.UpdatedAt, workspaceEntity.UpdatedAt);
        Assert.AreEqual(workspace.Name, workspaceEntity.Name);
        Assert.AreEqual(workspace.CreatedByUserEmail, workspaceEntity.FkCreatedByUserEmail);
    }

    [TestMethod]
    public void WorkspaceProfile_GivenWorkspaceEntity_MapsToWorkspaceModelCorrectly()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        string createdByUserEmail = "test@mail.com";
        WorkspaceEntity workspaceEntity = EntityHelper.GetDefaultWorkspaceEntity(id, createdByUserEmail);
        
        // Act
        Workspace workspace = _mapper.Map<Workspace>(workspaceEntity);
        
        // Assert
        Assert.AreEqual(workspaceEntity.Id, workspace.Id);
        Assert.AreEqual(workspaceEntity.CreatedAt, workspace.CreatedAt);
        Assert.AreEqual(workspaceEntity.UpdatedAt, workspace.UpdatedAt);
        Assert.AreEqual(workspaceEntity.Name, workspace.Name);
        Assert.AreEqual(workspaceEntity.FkCreatedByUserEmail, workspace.CreatedByUserEmail);
        Assert.IsNotNull(workspace.Tasks, "Tasks collection should not be null.");
        Assert.AreEqual(workspaceEntity.Tasks?.Count ?? 0, workspace.Tasks.Count, "Task count mismatch.");
    }
}