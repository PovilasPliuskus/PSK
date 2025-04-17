using AutoMapper;
using DataAccess.Entities;
using DataAccess.Profiles;
using DataAccess.Repositories;
using TestSupport.Helpers;
using Model = Contracts.Models;

namespace TaskManager.API.Tests.Repositories;

[TestClass]
public class TaskRepositoryTests
{
    private IMapper _mapper;

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
    public async Task GetAsync_GivenExistingTask_ReturnsItCorrectly()
    {
        await TransactionHelper.WrapInTransactionAndRollbackAsync(async context =>
        {
            //Arrange
            Guid id = Guid.NewGuid();
            Guid workspaceId = Guid.NewGuid();
            string createdByUserEmail = "test@test.mail";
            Model.Task task = ModelHelper.GetDefaultTaskModel(id, workspaceId, createdByUserEmail);

            TaskEntity taskEntity = _mapper.Map<TaskEntity>(task);

            await context.Tasks.AddAsync(taskEntity);
            await context.SaveChangesAsync();

            TaskRepository repository = new(context, _mapper);

            //Act
            Model.Task retrievedTask = await repository.GetAsync(id);

            //Assert
            Assert.AreEqual(retrievedTask.Id, task.Id);
            Assert.AreEqual(retrievedTask.CreatedAt, task.CreatedAt);
            Assert.AreEqual(retrievedTask.UpdatedAt, task.UpdatedAt);
            Assert.AreEqual(retrievedTask.Name, task.Name);
            Assert.AreEqual(retrievedTask.CreatedByUserEmail, task.CreatedByUserEmail);
            Assert.AreEqual(retrievedTask.WorkspaceId, task.WorkspaceId);
            Assert.AreEqual(retrievedTask.AssignedToUserEmail, task.AssignedToUserEmail);
            Assert.AreEqual(retrievedTask.DueDate, task.DueDate);
            Assert.AreEqual(retrievedTask.Status, task.Status);
            Assert.AreEqual(retrievedTask.Estimate, task.Estimate);
            Assert.AreEqual(retrievedTask.Type, task.Type);
            Assert.AreEqual(retrievedTask.Priority, task.Priority);
        });
    }
}
