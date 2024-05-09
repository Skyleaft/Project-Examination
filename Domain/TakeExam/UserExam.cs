using Domain.RoomSet;
using Domain.User;

namespace Domain.TakeExam;

public class UserExam
{
    public int Id { get; set; }
    public UserProfile User { get; set; }
    public bool IsOngoing { get; set; }
    public Room Room { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public List<UserAnswer> UserAnswers { get; set; }
    
}