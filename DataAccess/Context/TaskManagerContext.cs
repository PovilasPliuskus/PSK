using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Context;

public class TaskManagerContext(DbContextOptions<TaskManagerContext> options) : DbContext(options)
{
    public DbSet<AttachmentEntity>? Attachments { get; set; }
    public DbSet<CommentEntity>? Comments { get; set; }
    public DbSet<SubTaskEntity>? SubTasks { get; set; }
    public DbSet<TaskEntity>? Tasks { get; set; }
    public DbSet<WorkspaceEntity>? Workspaces { get; set; }
    public DbSet<WorkspaceUsersEntity>? WorkspaceUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // WorkspaceUsersEntity
        modelBuilder.Entity<WorkspaceUsersEntity>()
            .HasKey(wu => new { wu.FkUserEmail, wu.FkWorkspaceId });

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

        // SubtaskEntity
        modelBuilder.Entity<SubTaskEntity>()
            .HasOne(st => st.Task)
            .WithMany()
            .HasForeignKey(st => st.FkTaskId)
            .OnDelete(DeleteBehavior.Cascade);

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
    }

    // This overrides the SaveChangesAsync method to set the CreatedAt and UpdatedAt properties
    // for entities that inherit from BaseModelEntity.
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries()
        .Where(e => e.Entity is BaseModelEntity &&
                    (e.State == EntityState.Added || e.State == EntityState.Modified));

        foreach (var entry in entries)
        {
            var entity = (BaseModelEntity)entry.Entity;

            if (entry.State == EntityState.Added)
            {
                entity.CreatedAt = DateTime.UtcNow;
            }

            entity.UpdatedAt = DateTime.UtcNow;
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}