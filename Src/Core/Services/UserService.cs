using EvalApi.Src.Views.Dto; // Ajoutez cette ligne pour accéder à CreateUserDto
using EvalApi.Src.Models;
using EvalApi.Src.Core.Repositories;
using EvalApi.Src.Core.Repositories.Entities;

namespace EvalApi.Src.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserModel> CreateUserAsync(CreateUserDto userDto) // Utilisation de CreateUserDto ici
        {
            // Convertir CreateUserDto en UserEntity pour l'insertion dans la base de données
            var userEntity = new UserEntity
            {
                Name = userDto.Name,
                Username = userDto.Username,
                Email = userDto.Email
            };

            // Création de l'utilisateur dans la base de données
            var createdUser = await _userRepository.CreateUserAsync(userEntity);

            // Retourner un UserModel avec les données créées
            return new UserModel
            {
                Id = createdUser.Id,
                Name = createdUser.Name,
                Username = createdUser.Username,
                Email = createdUser.Email
            };
        }

        public async Task<List<UserModel>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return users.Select(u => new UserModel
            {
                Id = u.Id,
                Name = u.Name,
                Username = u.Username,
                Email = u.Email
            }).ToList();
        }

        public async Task<UserModel> GetUserByIdAsync(int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            return new UserModel
            {
                Id = user.Id,
                Name = user.Name,
                Username = user.Username,
                Email = user.Email
            };
        }
    }
}
