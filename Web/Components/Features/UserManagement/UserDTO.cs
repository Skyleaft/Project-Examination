using System.ComponentModel.DataAnnotations;
using Domain.Common;
using Web.Components.Features.Auth;

namespace Web.Components.Features.UserManagement;

public class UserDTO : LoginDTO
{
    [Required]
    public string NamaLengkap { get; set; }
    public DateTime? TangalLahir { get; set; }
    [Required]
    public Gender Gender { get; set; }
    public byte[]? Photo { get; set; }
    [Required]
    public string Email { get; set; }
    [MaxLength(14)]
    public string? NomorTelp { get; set; }
    public string Role { get; set; }
}