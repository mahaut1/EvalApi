using EvalApi.Src.Models;

namespace EvalApi.Src.Core.Services
{
    public interface IPostService
    {
        Task<PostModel> CreatePostAsync(PostModel post);
        Task<List<PostModel>> GetPostsByUserIdAsync(int userId);
        Task<PostModel> GetPostByIdAsync(int postId);
        Task<PostModel> UpdatePostAsync(PostModel post);
        Task DeletePostAsync(int postId);
    }
}
