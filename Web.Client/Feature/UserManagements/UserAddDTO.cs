using System.ComponentModel.DataAnnotations;

namespace Web.Client.Feature.UserManagements;

public class UserAddDTO: UserDTO
{
    [Required]
    [MinLength(4)]
    [MaxLength(40)]
    public override string UserName { get; set; } = null!;
    [Required]
    [MinLength(6)]
    [MaxLength(30)]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
}