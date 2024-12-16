using EvalApi.Src.Core.Repositories;
using EvalApi.Src.Core.Repositories.Entities;
using EvalApi.Src.Models;


namespace EvalApi.Src.Core.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;

        public PostService(IPostRepository postRepository, IUserRepository userRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        public async Task<PostModel> CreatePostAsync(PostModel post)
        {
            // Validation
            var user = await _userRepository.GetUserByIdAsync(post.UserId);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            var postEntity = new PostEntity
            {
                UserId = post.UserId,
                Title = post.Title,
                Body = post.Body
            };

            var createdPost = await _postRepository.CreatePostAsync(postEntity);

            return new PostModel
            {
                Id = createdPost.Id,
                UserId = createdPost.UserId,
                Title = createdPost.Title,
                Body = createdPost.Body
            };
        }

        public async Task<List<PostModel>> GetPostsByUserIdAsync(int userId)
        {
            var posts = await _postRepository.GetPostsByUserIdAsync(userId);
            return posts.Select(p => new PostModel
            {
                Id = p.Id,
                UserId = p.UserId,
                Title = p.Title,
                Body = p.Body
            }).ToList();
        }

        public async Task<PostModel> GetPostByIdAsync(int postId)
        {
            var post = await _postRepository.GetPostByIdAsync(postId);
            if (post == null)
            {
                throw new KeyNotFoundException("Post not found.");
            }

            return new PostModel
            {
                Id = post.Id,
                UserId = post.UserId,
                Title = post.Title,
                Body = post.Body
            };
        }

        public async Task<PostModel> UpdatePostAsync(PostModel post)
        {
            var existingPost = await _postRepository.GetPostByIdAsync(post.Id);
            if (existingPost == null)
            {
                throw new KeyNotFoundException("Post not found.");
            }

            if (existingPost.UserId != post.UserId)
            {
                throw new InvalidOperationException("UserId mismatch.");
            }

            existingPost.Title = post.Title;
            existingPost.Body = post.Body;

            var updatedPost = await _postRepository.UpdatePostAsync(existingPost);

            return new PostModel
            {
                Id = updatedPost.Id,
                UserId = updatedPost.UserId,
                Title = updatedPost.Title,
                Body = updatedPost.Body
            };
        }

        public async Task DeletePostAsync(int postId)
        {
            var post = await _postRepository.GetPostByIdAsync(postId);
            if (post == null)
            {
                throw new KeyNotFoundException("Post not found.");
            }

            await _postRepository.DeletePostAsync(postId);
        }
    }
}
