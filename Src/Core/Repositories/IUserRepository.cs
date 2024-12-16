using EvalApi.Src.Core.Repositories.Entities;

namespace EvalApi.Src.Core.Repositories
{
    public interface IUserRepository
    {
        Task<UserEntity> CreateUserAsync(UserEntity user);
        Task<List<UserEntity>> GetAllUsersAsync();
        Task<UserEntity> GetUserByIdAsync(int userId);
    }
}
