using Contracts.Models;

namespace Contracts.ResponseBodies;

public class GetWorkspacesResponse
{
    public List<WorkspaceWithoutTasks>? Workspaces { get; init; }
}