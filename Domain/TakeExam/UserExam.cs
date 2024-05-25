using Domain.RoomSet;
using Domain.Users;

namespace Domain.TakeExam;

public class UserExam
{
    public int Id { get; set; }
    public UserProfile? UserProfile { get; set; }
    public int? UserProfileId { get; set; }
    public bool IsOngoing { get; set; }
    public Room? Room { get; set; }
    public int? RoomId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public List<UserAnswer> UserAnswers { get; set; }
}