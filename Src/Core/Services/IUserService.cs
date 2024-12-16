using EvalApi.Src.Views.Dto; // Assurez-vous d'importer le namespace contenant CreateUserDto
using EvalApi.Src.Models;

namespace EvalApi.Src.Core.Services
{
    public interface IUserService
    {// Changer UserModel en CreateUserDto
        Task<List<UserModel>> GetAllUsersAsync();
        Task<UserModel> CreateUserAsync(CreateUserDto user); 
        Task<UserModel> GetUserByIdAsync(int userId);
    }
}
