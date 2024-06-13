using System.ComponentModel.DataAnnotations;
using Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace Domain.Users;

public class ApplicationUser :IdentityUser
{
    public string NamaLengkap { get; set; }
    public DateTime? TangalLahir { get; set; }
    [Required]
    public Gender Gender { get; set; }
    public byte[]? Photo { get; set; }
}