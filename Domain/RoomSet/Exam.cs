using System.IO.IsolatedStorage;

namespace Domain.RoomSet;

public class Exam
{
    public int Id { get; set; }
    public bool IsRandomize { get; set; }
    public List<Soal>? Soals { get; set; }

    public int TotalSoal
    {
        get { return TotalSoal; }
        private set
        {
            if (Soals != null)
                TotalSoal = Soals.Count;
            else TotalSoal = 0;
        }
    }
}