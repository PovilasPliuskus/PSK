using DataAccess.Repositories.Interfaces;
using AutoMapper;
using BusinessLogic.Interfaces;
using Contracts.RequestBodies;
using Contracts.ResponseBodies;
using Model = Contracts.Models;

namespace BusinessLogic.Services;

public class SubTaskService : ISubTaskService{
    private readonly ISubTaskRepository _subTaskRepository;
    private readonly IMapper _mapper;

    public SubTaskService(ISubTaskRepository subTaskRepository,
        IMapper mapper)
    {
        _subTaskRepository = subTaskRepository;
        _mapper = mapper;
    }

    public async Task<GetTaskSubTasksResponse> GetTaskSubTasksAsync(Guid taskId){
        List<Model.SubTask> subTasks = await _subTaskRepository.GetTaskSubtasks(taskId);

        return _mapper.Map<GetTaskSubTasksResponse>(subTasks);
    }


    public async Task CreateSubTaskAsync(CreateSubTaskRequest request){
        Model.SubTask subTask = _mapper.Map<Model.SubTask>(request);

        await _subTaskRepository.AddAsync(subTask);
    }

    public async Task DeleteSubTaskAsync(Guid id){
        await _subTaskRepository.RemoveAsync(id);
    }

    public async Task<GetSubTaskResponse> GetSubTaskAsync(Guid id){
        Model.SubTask subTask = await _subTaskRepository.GetAsync(id);

        return _mapper.Map<GetSubTaskResponse>(subTask);
    }

    public async Task UpdateSubTaskAsync(UpdateSubTaskRequest request){
        Model.SubTask subTask = _mapper.Map<Model.SubTask>(request);

        await _subTaskRepository.UpdateAsync(subTask);
    }
}