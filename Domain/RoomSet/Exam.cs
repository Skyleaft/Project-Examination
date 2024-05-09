namespace Domain.RoomSet;

public class Exam
{
    public int Id { get; set; }
    public bool IsRandomize { get; set; }
    public List<Soal> Soals { get; set; }
}