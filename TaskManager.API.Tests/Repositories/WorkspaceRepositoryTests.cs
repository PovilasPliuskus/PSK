using AutoMapper;
using Model = Contracts.Models;
using DataAccess.Entities;
using DataAccess.Profiles;
using DataAccess.Repositories;
using TestSupport.Helpers;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.API.Tests.Repositories;

[TestClass]
public class WorkspaceRepositoryTests
{
    private IMapper _mapper;

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
    public async Task AddAsync_GivenValidWorkspace_AddsToDatabase()
    {
        await TransactionHelper.WrapInTransactionAndRollbackAsync(async context =>
        {
            // Arrange
            Guid id = Guid.NewGuid();
            Model.WorkspaceWithoutTasks workspace = ModelHelper.GetDefaultWorkspaceWithoutTasksModel(id);

            WorkspaceRepository repository = new(context, _mapper);

            // Act
            await repository.AddAsync(workspace);

            // Assert
            Assert.IsNotNull(await context.Workspaces.FindAsync(id));
        });
    }
    
    [TestMethod]
    public async Task GetAsync_GivenExistingWorkspace_ReturnsItCorrectly()
    {
        await TransactionHelper.WrapInTransactionAndRollbackAsync(async context =>
        {
            // Arrange
            Guid id = Guid.NewGuid();
            Guid taskId = Guid.NewGuid();
            string createdByUserEmail = "test@test.com";
            
            Model.Task task = ModelHelper.GetDefaultTaskModel(taskId, id, createdByUserEmail);
            Model.Workspace workspace = ModelHelper.GetDefaultWorkspaceModel(id, task);
            
            WorkspaceEntity workspaceEntity = _mapper.Map<WorkspaceEntity>(workspace);

            await context.Workspaces.AddAsync(workspaceEntity);
            await context.SaveChangesAsync();

            WorkspaceRepository repository = new(context, _mapper);

            // Act
            Model.Workspace retrievedWorkspace = await repository.GetSingleAsync(id);

            // Assert
            Assert.AreEqual(workspace.Id, retrievedWorkspace.Id);
            Assert.AreEqual(workspace.CreatedAt, retrievedWorkspace.CreatedAt);
            Assert.AreEqual(workspace.UpdatedAt, retrievedWorkspace.UpdatedAt);
            Assert.AreEqual(workspace.Name, retrievedWorkspace.Name);
            Assert.AreEqual(workspace.CreatedByUserEmail, retrievedWorkspace.CreatedByUserEmail);
            Assert.IsNotNull(workspace.Tasks, "Tasks collection should not be null.");
            Assert.AreEqual(workspaceEntity.Tasks?.Count ?? 0, workspace.Tasks.Count, "Task count mismatch.");
        });
    }
    
    // TODO: Finish writing tests for the remaining repository methods
}