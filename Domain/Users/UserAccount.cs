using System.ComponentModel.DataAnnotations;

namespace Domain.Users;

public class UserAccount
{
    public int Id { get; set; }
    [Required]
    [StringLength(20, ErrorMessage = "Username Tidak Boleh lebih dari 20 character")]
    public string Username { get; set; }
    [Required]
    [StringLength(35, ErrorMessage = "Password minimal 6 character dan kurang dari 35", MinimumLength = 6)]
    public string Password { get; set; }
    public bool IsActive { get; set; }
    public string? Email { get; set; }
    public string? IPAddress { get; set; }
    public string? Device { get; set; }
    public DateTime? LastLogin { get; set; }
}