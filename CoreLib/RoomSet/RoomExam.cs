using CoreLib.TakeExam;
using Microsoft.EntityFrameworkCore;
using CoreLib.Users;

namespace CoreLib.RoomSet;

public class RoomExam
{
    public Guid RoomId { get; set; }
    public Room? Room { get; set; }
    public IEnumerable<UserExam>? UserExams { get; set; }
    public int? TotalPeserta => UserExams?.Count() ?? 0;
}