using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Context;

public class TaskManagerContext(DbContextOptions<TaskManagerContext> options) : DbContext(options)
{
    public DbSet<AttachmentEntity>? Attachments { get; set; }
    public DbSet<CommentEntity>? Comments { get; set; }
    public DbSet<SubTaskEntity>? SubTasks { get; set; }
    public DbSet<TaskEntity>? Tasks { get; set; }
    public DbSet<UserEntity>? Users { get; set; }
    public DbSet<WorkspaceEntity>? Workspaces { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }
}
