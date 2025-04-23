using AutoMapper;
using DataAccess.Entities;
using DataAccess.Profiles;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
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
    public async Task AddAsync_GivenValidTask_AddsToDatabase()
    {
        await TransactionHelper.WrapInTransactionAndRollbackAsync(async context =>
        {
            //Arrange
            Guid id = Guid.NewGuid();
            Guid workspaceId = Guid.NewGuid();
            string createdByUserEmail = "test@test.mail";
            Model.Task task = ModelHelper.GetDefaultTaskModel(id, workspaceId, createdByUserEmail);

            TaskRepository repository = new(context, _mapper);

            //Act
            await repository.AddAsync(task);

            //Assert
            Assert.IsNotNull(await context.Tasks.FindAsync(id));
        });
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

    [TestMethod]
    public async Task RemoveAsync_GivenExistingTask_RemovesFromDatabase()
    {
        await TransactionHelper.WrapInTransactionAndRollbackAsync(async context =>
        {
            //Arrange
            Guid workspaceId = Guid.NewGuid();
            string createdByUserEmail = "test@test.mail";
            TaskEntity taskEntity = EntityHelper.GetDefaultTaskEntity(workspaceId, createdByUserEmail);
            Guid id = taskEntity.Id;

            await context.Tasks.AddAsync(taskEntity);
            await context.SaveChangesAsync();

            context.Entry(taskEntity).State = EntityState.Detached;

            TaskRepository repository = new(context, _mapper);

            //Act
            await repository.RemoveAsync(id);

            //Act & Assert
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(async () =>
            {
                await repository.GetAsync(id);
            });
        });
    }

    [TestMethod]
    public async Task GetAllFromWorkspaceAsync()
    {
        await TransactionHelper.WrapInTransactionAndRollbackAsync(async context =>
        {
            //Arrange
            int firstWorkspaceTaskCount = 5;
            int secondWorkspaceTaskCount = 3;
            Guid firstWorkspaceId = Guid.NewGuid();
            Guid secondWorkspaceId = Guid.NewGuid();
            List<Model.Task> firstWorkspaceTasks = [];
            List<Model.Task> secondWorkspaceTasks = [];
            string createdByUserEmail = "test@test.mail";

            for (int i = 0; i < firstWorkspaceTaskCount; i++)
            {
                Model.Task task = ModelHelper.GetDefaultTaskModel(Guid.NewGuid(), firstWorkspaceId, createdByUserEmail);
                firstWorkspaceTasks.Add(task);

                TaskEntity taskEntity = _mapper.Map<TaskEntity>(task);
                await context.Tasks.AddAsync(taskEntity);
            }

            for (int i = 0; i < secondWorkspaceTaskCount; i++)
            {
                Model.Task task = ModelHelper.GetDefaultTaskModel(Guid.NewGuid(), secondWorkspaceId, createdByUserEmail);
                secondWorkspaceTasks.Add(task);

                TaskEntity taskEntity = _mapper.Map<TaskEntity>(task);
                await context.Tasks.AddAsync(taskEntity);
            }

            await context.SaveChangesAsync();

            TaskRepository repository = new(context, _mapper);

            //Act
            List<Model.Task> firstRetrievedTasks = await repository.GetAllFromWorkspaceAsync(firstWorkspaceId);
            List<Model.Task> secondRetrievedTasks = await repository.GetAllFromWorkspaceAsync(secondWorkspaceId);

            //Assert
            Assert.AreEqual(firstWorkspaceTaskCount, firstRetrievedTasks.Count);
            Assert.AreEqual(secondWorkspaceTaskCount, secondRetrievedTasks.Count);
        });
    }
}
