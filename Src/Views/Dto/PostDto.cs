using System.ComponentModel.DataAnnotations;

namespace EvalApi.Src.Views.Dto;

public class PostDto
{
    [Required, Range(1, int.MaxValue)] public required int id { get; init; }
    [Required] public required string title { get; init; }
    [Required] public required string body { get; init; }
    [Required, Range(1, int.MaxValue)] public required int userId { get; init; }
}