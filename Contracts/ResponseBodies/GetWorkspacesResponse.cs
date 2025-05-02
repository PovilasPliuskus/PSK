using Contracts.Models;
using Contracts.Pagination;

namespace Contracts.ResponseBodies;

public class GetWorkspacesResponse
{
    public PaginatedResult<WorkspaceWithoutTasks>? Workspaces { get; init; }
}