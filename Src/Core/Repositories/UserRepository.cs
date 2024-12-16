using EvalApi.Src.Core.Repositories.Entities;
using Microsoft.EntityFrameworkCore;



namespace EvalApi.Src.Core.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;

        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserEntity> CreateUserAsync(UserEntity user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<List<UserEntity>> GetAllUsersAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<UserEntity> GetUserByIdAsync(int userId)
        {
            return await _dbContext.Users.Include(u => u.Posts).FirstOrDefaultAsync(u => u.Id == userId);
        }
    }
}
