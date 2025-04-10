using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Context;

public class TaskManagerContext : DbContext
{
    public TaskManagerContext(DbContextOptions<TaskManagerContext> options) : base(options) { }

    public DbSet<AttachmentEntity> Attachments { get; set; } = null!;
    public DbSet<CommentEntity> Comments { get; set; } = null!;
    public DbSet<SubTaskEntity> SubTasks { get; set; } = null!;
    public DbSet<TaskEntity> Tasks { get; set; } = null!;
    public DbSet<UserEntity> Users { get; set; } = null!;
    public DbSet<WorkspaceEntity> Workspaces { get; set; } = null!;
    public DbSet<WorkspaceUsersEntity> WorkspaceUsers { get; set; } = null!;

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

        // CommentEntity
        modelBuilder.Entity<CommentEntity>()
            .HasOne(c => c.Task)
            .WithMany()
            .HasForeignKey(c => c.FkTaskId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CommentEntity>()
            .HasOne(c => c.SubTask)
            .WithMany()
            .HasForeignKey(c => c.FkSubTaskId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CommentEntity>()
            .HasOne(c => c.WrittenByUserId)
            .WithMany()
            .HasForeignKey(c => c.FkWrittenByUserId)
            .OnDelete(DeleteBehavior.Cascade);

        // AttachmentEntity
        modelBuilder.Entity<AttachmentEntity>()
            .HasOne(a => a.Task)
            .WithMany()
            .HasForeignKey(a => a.FkTaskId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<AttachmentEntity>()
            .HasOne(a => a.SubTask)
            .WithMany()
            .HasForeignKey(a => a.FkSubTaskId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<AttachmentEntity>()
            .HasOne(a => a.CreatedByUserId)
            .WithMany()
            .HasForeignKey(a => a.FkCreatedByUserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
