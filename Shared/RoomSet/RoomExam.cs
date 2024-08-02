using Microsoft.EntityFrameworkCore;
using Shared.TakeExam;
using Shared.Users;

namespace Shared.RoomSet;

public class RoomExam
{
    public Guid RoomId { get; set; }
    public Room? Room { get; set; }
    public IEnumerable<UserExam>? UserExams { get; set; }
    public int? TotalPeserta => UserExams?.Count() ?? 0;
}