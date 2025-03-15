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
    public DbSet<WorkspaceUsersEntity>? WorkspaceUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // UserEntity
        modelBuilder.Entity<UserEntity>()
            .HasIndex(u => u.StudentId)
            .IsUnique();

        modelBuilder.Entity<UserEntity>()
            .HasIndex(u => u.Email)
            .IsUnique();

        // WorkspaceEntity
        modelBuilder.Entity<WorkspaceEntity>()
            .HasOne(w => w.CreatedByUserId)
            .WithMany()
            .HasForeignKey(w => w.FkCreatedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        // WorkspaceUsersEntity
        modelBuilder.Entity<WorkspaceUsersEntity>()
            .HasKey(wu => new { wu.FkUserId, wu.FkWorkspaceId });

        modelBuilder.Entity<WorkspaceUsersEntity>()
            .HasOne<UserEntity>()
            .WithMany()
            .HasForeignKey(wu => wu.FkUserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<WorkspaceUsersEntity>()
            .HasOne<WorkspaceEntity>()
            .WithMany()
            .HasForeignKey(wu => wu.FkWorkspaceId)
            .OnDelete(DeleteBehavior.Cascade);

        // TaskEntity
        modelBuilder.Entity<TaskEntity>()
            .HasOne(t => t.Workspace)
            .WithMany()
            .HasForeignKey(t => t.FkWorkspaceId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TaskEntity>()
            .HasOne(t => t.CreatedByUserId)
            .WithMany()
            .HasForeignKey(t => t.FkCreatedByUserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TaskEntity>()
            .HasOne(t => t.AssignedToUserId)
            .WithMany()
            .HasForeignKey(t => t.FkAssignedToUserId)
            .OnDelete(DeleteBehavior.SetNull);

        // SubtaskEntity
        modelBuilder.Entity<SubTaskEntity>()
            .HasOne(st => st.Task)
            .WithMany()
            .HasForeignKey(st => st.FkTaskId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<SubTaskEntity>()
            .HasOne(st => st.CreatedByUserId)
            .WithMany()
            .HasForeignKey(st => st.FkCreatedByUserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<SubTaskEntity>()
            .HasOne(st => st.AssignedToUserId)
            .WithMany()
            .HasForeignKey(st => st.FkAssignedToUserId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
