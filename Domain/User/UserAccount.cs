namespace Domain.User;

public class UserAccount
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public UserProfile Profile { get; set; }
    public string IPAddress { get; set; }
    public string Device { get; set; }
}