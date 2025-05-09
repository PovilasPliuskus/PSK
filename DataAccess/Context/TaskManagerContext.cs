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

        // Task <-> Workspace
        modelBuilder.Entity<TaskEntity>()
            .HasOne(t => t.Workspace)
            .WithMany(w => w.Tasks)
            .HasForeignKey(t => t.FkWorkspaceId)
            .OnDelete(DeleteBehavior.Cascade);

        // Task <-> SubTask
        modelBuilder.Entity<TaskEntity>()
            .HasMany(t => t.SubTasks)
            .WithOne(st => st.Task)
            .HasForeignKey(st => st.FkTaskId)
            .OnDelete(DeleteBehavior.Cascade);

        // Comment <-> SubTask
        modelBuilder.Entity<CommentEntity>()
            .HasOne(c => c.SubTask)
            .WithMany(st => st.Comments)
            .HasForeignKey(c => c.FkSubTaskId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);

        // Comment <-> Task
        modelBuilder.Entity<CommentEntity>()
            .HasOne(c => c.Task)
            .WithMany(t => t.Comments)
            .HasForeignKey(c => c.FkTaskId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);

        // Attachment <-> Task
        modelBuilder.Entity<AttachmentEntity>()
            .HasOne(a => a.Task)
            .WithMany(t => t.Attachments)
            .HasForeignKey(a => a.FkTaskId)
            .OnDelete(DeleteBehavior.Cascade);

        // Attachment <-> SubTask
        modelBuilder.Entity<AttachmentEntity>()
            .HasOne(a => a.SubTask)
            .WithMany(st => st.Attachments)
            .HasForeignKey(a => a.FkSubTaskId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TaskEntity>()
            .Property(v => v.Version)
            .IsRowVersion();

        modelBuilder.Entity<SubTaskEntity>()
            .Property(v => v.Version)
            .IsRowVersion();

        modelBuilder.Entity<WorkspaceEntity>()
            .Property(v => v.Version)
            .IsRowVersion();
        
        modelBuilder.Entity<CommentEntity>()
            .Property(v => v.Version)
            .IsRowVersion();
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