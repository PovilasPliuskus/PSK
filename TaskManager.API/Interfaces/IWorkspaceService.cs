public interface IWorkspaceService
{
    Task<bool> DoesWorkspaceExistAsync(Guid workspaceId);
}