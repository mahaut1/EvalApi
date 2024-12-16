using System.ComponentModel.DataAnnotations;

namespace EvalApi.Src.Views.Dto;

public class CreateUserDto
{
  [Required] public required string Name { get; init; }
  [Required] public required string Username { get; init; }
  [Required, EmailAddress] public required string Email { get; init; }
}