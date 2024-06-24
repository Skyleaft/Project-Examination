namespace Shared.BankSoal;

public class Soal
{
    public Guid Id { get; set; }
    public int ExamId { get; set; }
    public int Nomor { get; set; }
    public string Pertanyaan { get; set; }
    public List<SoalJawaban> PilihanJawaban { get; set; }
    public bool isMultipleJawaban { get; set; }
    public int BobotPoint
    {
        get
        {
            if (PilihanJawaban != null)
                return PilihanJawaban.Sum(x => x.Point);
            return 0;
        }
    }
}
