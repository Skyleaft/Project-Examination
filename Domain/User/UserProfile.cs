namespace Domain.User;

public class UserProfile
{
    public int Id { get; set; }
    public string NamaLengkap { get; set; }
    public DateTime TangalLahir { get; set; }
    public string NoTelp { get; set; }
    public string Photo { get; set; }
    public Role Role { get; set; }
    public UserAccount? UserAccount { get; set; }
}