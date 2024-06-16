using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Shared.Common;

namespace Shared.Users;

public class ApplicationUser :IdentityUser
{
    public string NamaLengkap { get; set; }
    [Required]
    public Gender Gender { get; set; }
    public byte[]? Photo { get; set; }
    public string? Pekerjaan { get; set; }
    public Kota? Kota { get; set; }
    public string? KotaId { get; set; }
}