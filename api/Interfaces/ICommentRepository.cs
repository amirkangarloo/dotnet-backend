using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dto.Comment;
using api.Models;

namespace api.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();
        Task<Comment?> GetOneAsync(int id);
        Task<Comment?> CreateAsync(Comment commentModel);
        Task<Comment> UpdateAsync(int id, UpdateCommentRequestDto commentDto);
        Task<Comment?> DeleteAsync(int id);
    }
}