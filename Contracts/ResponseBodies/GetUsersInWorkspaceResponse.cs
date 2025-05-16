using Contracts.Models;
using Contracts.Pagination;

namespace Contracts.ResponseBodies;

public class GetUsersInWorkspaceResponse
{
    public PaginatedResult<WorkspaceUser>? WorkspacesUsers { get; init; }
}