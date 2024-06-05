using System.ComponentModel.DataAnnotations;
using Domain.Common;
using Domain.Users;

namespace Web.Components.Features.UserManagement;

public class UserDTO
{
    [Required]
    [StringLength(40, ErrorMessage = "Nama Tidak Boleh lebih dari 40 character")]
    public string NamaLengkap { get; set; }
    public DateTime? TangalLahir { get; set; }
    [Required]
    public Gender Gender { get; set; }
    [StringLength(15, ErrorMessage = "Nama Tidak Boleh lebih dari 15 character")]
    public string? NoTelp { get; set; }
    public string? Photo { get; set; }
    public int RoleId { get; set; }
    [Required]
    [StringLength(20, ErrorMessage = "Username Tidak Boleh lebih dari 20 character")]
    public string Username { get; set; }
    [Required]
    [StringLength(35, ErrorMessage = "Password minimal 6 character dan kurang dari 35", MinimumLength = 6)]
    public string Password { get; set; }
    public bool IsActive { get; set; }
    public string? Email { get; set; }
}