using AutoMapper;
using Contracts.Models;
using DataAccess.Context;
using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace DataAccess.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly TaskManagerContext _context;
    private readonly IMapper _mapper;

    public CommentRepository(TaskManagerContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<List<Comment>> GetRangeAsync(Guid taskId)
    {
        var commentEntities = await _context.Comments
            .Where(c => c.FkTaskId == taskId)
            .ToListAsync();

        return _mapper.Map<List<Comment>>(commentEntities);
    }
    
    public async Task<Comment> GetSingleAsync(Guid commentId)
    {
        var commentEntity = await _context.Comments
            .FirstOrDefaultAsync(c => c.Id == commentId);

        if (commentEntity == null)
            throw new KeyNotFoundException($"Comment with id {commentId} not found.");

        return _mapper.Map<Comment>(commentEntity);
    }
    
    public async Task AddAsync(Comment comment)
    {
        var commentEntity = _mapper.Map<CommentEntity>(comment);
        
        _context.Comments.Add(commentEntity);
        
        await _context.SaveChangesAsync();
    }
    
    public async Task RemoveAsync(Guid commentId)
    {
        var commentEntity = await _context.Comments.FindAsync(commentId);

        if (commentEntity == null)
            throw new KeyNotFoundException($"Comment with id {commentId} not found.");

        _context.Comments.Remove(commentEntity);
        
        await _context.SaveChangesAsync();
    }
}