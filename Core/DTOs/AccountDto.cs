using System.ComponentModel.DataAnnotations;

namespace api.Core.DTOs;

public class AccountCreateDto
{

    [Required]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    [StringLength(100, MinimumLength = 6)]
    public string Password { get; set; } = null!;

    [Required]
    public int UserId { get; set; }

    [Required]
    public string Role { get; set; } = null!;

    public string Gender { get; set; } = "Unspecified";
}

public class AccountUpdateDto
{
    [EmailAddress]
    public string? Email { get; set; }

    [StringLength(100, MinimumLength = 6)]
    public string? Password { get; set; }

    public string? Role { get; set; }
    public string? AccountStatus { get; set; }
}

public class AccountResponseDto
{
    public int Id { get; set; }
    public UserResponseDto? UserInfo { get; set; }
    public string Email { get; set; } = null!;
    public string Role { get; set; }
    public DateTime? LastLogin { get; set; }
    public string AccountStatus { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
