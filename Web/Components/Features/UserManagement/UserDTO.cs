using System.ComponentModel.DataAnnotations;
using Shared.Common;
using Web.Components.Features.Auth;

namespace Web.Components.Features.UserManagement;

public class UserDTO : LoginDTO
{
    [Required]
    public string NamaLengkap { get; set; }
    [Required]
    public Gender Gender { get; set; }
    public string? Pekerjaan { get; set; }
    public byte[]? Photo { get; set; }
    [Required]
    public string Email { get; set; }
    [MaxLength(14)]
    public string? PhoneNumber { get; set; }
    public string Role { get; set; }
    public string KotaId { get; set; }
}