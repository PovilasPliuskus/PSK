using AutoMapper;
using Contracts.Pagination;
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
    public async Task GetRangeAsync_ReturnsRangeOfWorkspacesCorrectly()
    {
        await TransactionHelper.WrapInTransactionAndRollbackAsync(async context =>
        {
            // Arrange
            Guid id1 = Guid.NewGuid();
            Guid id2 = Guid.NewGuid();
            Model.WorkspaceWithoutTasks workspace1 = ModelHelper.GetDefaultWorkspaceWithoutTasksModel(id1);
            Model.WorkspaceWithoutTasks workspace2 = ModelHelper.GetDefaultWorkspaceWithoutTasksModel(id2);

            WorkspaceEntity workspaceEntity1 = _mapper.Map<WorkspaceEntity>(workspace1);
            WorkspaceEntity workspaceEntity2 = _mapper.Map<WorkspaceEntity>(workspace2);
            
            await context.Workspaces.AddRangeAsync(workspaceEntity1, workspaceEntity2);
            await context.SaveChangesAsync();
            
            WorkspaceRepository repository = new(context, _mapper);
            
            // Act
            PaginatedResult<Model.WorkspaceWithoutTasks> paginatedResult = await repository.GetRangeAsync(1, 5, "fakeuser@mail.com");
            List<Model.WorkspaceWithoutTasks> workspaces = paginatedResult.Items.ToList();

            // Assert
            Assert.AreEqual(2, workspaces.Count, $"Expected 2 workspaces but got {workspaces.Count}.");
            Assert.AreEqual(workspaceEntity1.Id, workspaces[0].Id);
            Assert.AreEqual(workspaceEntity1.CreatedAt, workspaces[0].CreatedAt);
            Assert.AreEqual(workspaceEntity1.UpdatedAt, workspaces[0].UpdatedAt);
            Assert.AreEqual(workspaceEntity1.Name, workspaces[0].Name);
            Assert.AreEqual(workspaceEntity1.FkCreatedByUserEmail, workspaces[0].CreatedByUserEmail);
            Assert.AreEqual(workspaceEntity2.Id, workspaces[1].Id);
            Assert.AreEqual(workspaceEntity2.CreatedAt, workspaces[1].CreatedAt);
            Assert.AreEqual(workspaceEntity2.UpdatedAt, workspaces[1].UpdatedAt);
            Assert.AreEqual(workspaceEntity2.Name, workspaces[1].Name);
            Assert.AreEqual(workspaceEntity2.FkCreatedByUserEmail, workspaces[1].CreatedByUserEmail);
        });
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
    public async Task GetSingleAsync_GivenExistingWorkspace_ReturnsItCorrectly()
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