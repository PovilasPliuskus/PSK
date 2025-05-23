﻿using AutoMapper;
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

    public async Task<GetWorkspaceTasksResponse> GetWorkspaceTasksAsync(Guid workspaceId)
    {
        List<Model.Task> tasks = await _taskRepository.GetAllFromWorkspaceAsync(workspaceId);

        return _mapper.Map<GetWorkspaceTasksResponse>(tasks);
    }

    public async Task<GetTaskResponse> GetTaskAsync(Guid id)
    {
        Model.Task task = await _taskRepository.GetAsync(id);

        return _mapper.Map<GetTaskResponse>(task);
    }

    public async Task UpdateTaskAsync(UpdateTaskRequest request)
    {
        Model.Task task = _mapper.Map<Model.Task>(request);

        await _taskRepository.UpdateAsync(task);
    }
}
