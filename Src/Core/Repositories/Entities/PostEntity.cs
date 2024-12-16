namespace EvalApi.Src.Core.Repositories.Entities
{
    public class PostEntity
    {
        public int Id { get; set; } // Clé primaire
        public int UserId { get; set; } // Clé étrangère associée à l'utilisateur
        public string Title { get; set; } = string.Empty; // Titre de l'article, obligatoire
        public string Body { get; set; } = string.Empty; // Contenu de l'article, obligatoire

        // Navigation : Référence vers l'utilisateur auteur de l'article
        public UserEntity? User { get; set; }
    }
}
