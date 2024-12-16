using EvalApi.Src.Core.Repositories.Entities;

namespace EvalApi.Src.Core.Repositories
{
    public interface IPostRepository
    {
        Task<PostEntity> CreatePostAsync(PostEntity post);
        Task<List<PostEntity>> GetPostsByUserIdAsync(int userId);
        Task<PostEntity> GetPostByIdAsync(int postId);
        Task<PostEntity> UpdatePostAsync(PostEntity post);
        Task DeletePostAsync(int postId);
    }
}
