namespace Shared.BankSoal;

public class Soal
{
    public int Id { get; set; }
    public int ExamId { get; set; }
    public string Pertanyaan { get; set; }
    public List<SoalJawaban> PilihanJawaban { get; set; }
    public int BobotPoint { get; set; }
}