using System.ComponentModel.DataAnnotations;
using Domain.Common;

namespace Domain.Users;

public class UserProfile : IGenericModifier
{
    public int Id { get; set; }
    [Required]
    [StringLength(40, ErrorMessage = "Nama Tidak Boleh lebih dari 40 character")]
    public string NamaLengkap { get; set; }
    public DateTime? TangalLahir { get; set; }
    [Required]
    public Gender Gender { get; set; }
    [StringLength(15, ErrorMessage = "Nama Tidak Boleh lebih dari 15 character")]
    public string? NoTelp { get; set; }
    public string? Photo { get; set; }
    public Role? Role { get; set; }
    public int RoleId { get; set; }
    public UserAccount UserAccount { get; set; }
    public int UserAccountId { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? CreatedOn { get; set; }
    public string? LastModifiedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
}