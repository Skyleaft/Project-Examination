namespace Domain.Users;

public class UserSession
{
    public int Id { get; set; }
    public UserProfile  UserProfile { get; set; }
    public DateTime Expired { get; set; }
}