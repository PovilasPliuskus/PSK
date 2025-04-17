﻿using Contracts.RequestBodies;

namespace BusinessLogic.Interfaces
{
    public interface ITaskService
    {
        Task CreateTaskAsync(CreateTaskRequest request);
        Task DeleteTaskAsync(Guid id);
    }
}
