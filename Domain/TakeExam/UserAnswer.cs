using Domain.RoomSet;

namespace Domain.TakeExam;

public class UserAnswer
{
    public int Id { get; set; }
    public int UserExamId { get; set; }
    public Soal Soal { get; set; }
    public string Answer { get; set; }
}