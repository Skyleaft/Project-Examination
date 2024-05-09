namespace Domain.RoomSet;

public class Room
{
    public int Id { get; set; }
    public string Nama { get; set; }
    public string Kode { get; set; }
    public DateTime Jadwal { get; set; }
    public bool IsActive { get; set; }
    public Exam Exam { get; set; }
}