using AutoMapper;
using BusinessLogic.Interfaces;
using Contracts.RequestBodies;
using Contracts.ResponseBodies;
using DataAccess.Repositories.Interfaces;
using Model = Contracts.Models;

namespace BusinessLogic.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;
    private readonly IMapper _mapper;

    public TaskService(ITaskRepository taskRepository,
        IMapper mapper)
    {
        _taskRepository = taskRepository;
        _mapper = mapper;
    }

    public async Task CreateTaskAsync(CreateTaskRequest request)
    {
        Model.Task task = _mapper.Map<Model.Task>(request);

        await _taskRepository.AddAsync(task);
    }

    public async Task DeleteTaskAsync(Guid id)
    {
        await _taskRepository.RemoveAsync(id);
    }

    public async Task<GetWorkspaceTasksResponse> GetWorkspaceTasks(Guid workspaceId)
    {
        List<Model.Task> tasks = await _taskRepository.GetAllFromWorkspaceAsync(workspaceId);

        return _mapper.Map<GetWorkspaceTasksResponse>(tasks);
    }
}
