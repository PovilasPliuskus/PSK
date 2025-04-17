using AutoMapper;
using Model = Contracts.Models;
using DataAccess.Profiles;
using TestSupport.Helpers;
using DataAccess.Entities;

namespace TaskManager.API.Tests.Profiles;

[TestClass]
public class TaskProfileTests
{
    private IMapper _mapper = null!;

    [TestInitialize]
    public void Setup()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<TaskProfile>();
        });

        _mapper = config.CreateMapper();
    }

    [TestMethod]
    public void TaskProfile_GivenTaskModel_MapsToEntityCorrectly()
    {
        //Arrange
        Guid id = Guid.NewGuid();
        Guid workspaceId = Guid.NewGuid();
        string createdByUserEmail = "test@mail.com";
        Model.Task task = ModelHelper.GetDefaultTaskModel(id, workspaceId, createdByUserEmail);

        //Act
        TaskEntity taskEntity = _mapper.Map<TaskEntity>(task);

        //Assert
        Assert.AreEqual(task.Id, taskEntity.Id);
        Assert.AreEqual(task.CreatedAt, taskEntity.CreatedAt);
        Assert.AreEqual(task.UpdatedAt, taskEntity.UpdatedAt);
        Assert.AreEqual(task.Name, taskEntity.Name);
        Assert.AreEqual(task.CreatedByUserEmail, taskEntity.FkCreatedByUserEmail);
        Assert.AreEqual(task.WorkspaceId, taskEntity.FkWorkspaceId);
        Assert.AreEqual(task.AssignedToUserEmail, taskEntity.FkAssignedToUserEmail);
        Assert.AreEqual(task.DueDate, taskEntity.DueDate);
        Assert.AreEqual(task.Status, taskEntity.Status);
        Assert.AreEqual(task.Estimate, taskEntity.Estimate);
        Assert.AreEqual(task.Type, taskEntity.Type);
        Assert.AreEqual(task.Priority, taskEntity.Priority);
    }

    [TestMethod]
    public void TaskProfile_GivenTaskEntity_MapsToModelCorrectly()
    {
        //Arrange
        Guid workspaceId = Guid.NewGuid();
        string createdByUserEmail = "test@mail.com";
        TaskEntity taskEntity = EntityHelper.GetDefaultTaskEntity(workspaceId, createdByUserEmail);

        //Act
        Model.Task task = _mapper.Map<Model.Task>(taskEntity);

        //Assert
        Assert.AreEqual(task.Id, taskEntity.Id);
        Assert.AreEqual(task.CreatedAt, taskEntity.CreatedAt);
        Assert.AreEqual(task.UpdatedAt, taskEntity.UpdatedAt);
        Assert.AreEqual(task.Name, taskEntity.Name);
        Assert.AreEqual(task.CreatedByUserEmail, taskEntity.FkCreatedByUserEmail);
        Assert.AreEqual(task.WorkspaceId, taskEntity.FkWorkspaceId);
        Assert.AreEqual(task.AssignedToUserEmail, taskEntity.FkAssignedToUserEmail);
        Assert.AreEqual(task.DueDate, taskEntity.DueDate);
        Assert.AreEqual(task.Status, taskEntity.Status);
        Assert.AreEqual(task.Estimate, taskEntity.Estimate);
        Assert.AreEqual(task.Type, taskEntity.Type);
        Assert.AreEqual(task.Priority, taskEntity.Priority);
    }
}
