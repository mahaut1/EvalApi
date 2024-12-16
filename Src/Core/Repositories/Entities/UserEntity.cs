namespace EvalApi.Src.Core.Repositories.Entities
{
    public class UserEntity
    {
        public int Id { get; set; } // Clé primaire
        public string Name { get; set; } = string.Empty; // Nom de l'utilisateur, obligatoire
        public string Username { get; set; } = string.Empty; // Pseudo de l'utilisateur, obligatoire
        public string Email { get; set; } = string.Empty; // Email de l'utilisateur, obligatoire

        // Navigation : Liste des articles associés à l'utilisateur
        public List<PostEntity> Posts { get; set; } = new();
    }
}
