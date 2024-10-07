using System.ComponentModel.DataAnnotations;

namespace WebApi.Domain;

public class User
{
	public int Id { get; init; }

	[MaxLength(255)]
	public string? Username { get; set; }

	[MaxLength(255)]
	public string? PasswordHash { get; set; }
}