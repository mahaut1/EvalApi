using System.ComponentModel.DataAnnotations;

namespace EvalApi.Src.Models;

public class PostModel
{
    [Required, Range(1, int.MaxValue)] public int Id { get; set; }
    [Required] public required string Title { get; set; }
    [Required] public required string Body { get; set; }
    [Required, Range(1, int.MaxValue)] public int UserId { get; set; }
    public UserModel? User { get; set; }
}