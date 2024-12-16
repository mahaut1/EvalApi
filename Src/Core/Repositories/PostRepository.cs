using Microsoft.EntityFrameworkCore;
using EvalApi.Src.Core.Repositories.Entities;

namespace EvalApi.Src.Core.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly AppDbContext _dbContext;

        public PostRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PostEntity> CreatePostAsync(PostEntity post)
        {
            _dbContext.Posts.Add(post);
            await _dbContext.SaveChangesAsync();
            return post;
        }

        public async Task<List<PostEntity>> GetPostsByUserIdAsync(int userId)
        {
            return await _dbContext.Posts.Where(p => p.UserId == userId).ToListAsync();
        }

        public async Task<PostEntity> GetPostByIdAsync(int postId)
        {
            return await _dbContext.Posts.FirstOrDefaultAsync(p => p.Id == postId);
        }

        public async Task<PostEntity> UpdatePostAsync(PostEntity post)
        {
            _dbContext.Posts.Update(post);
            await _dbContext.SaveChangesAsync();
            return post;
        }

        public async Task DeletePostAsync(int postId)
        {
            var post = await _dbContext.Posts.FirstOrDefaultAsync(p => p.Id == postId);
            if (post != null)
            {
                _dbContext.Posts.Remove(post);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
