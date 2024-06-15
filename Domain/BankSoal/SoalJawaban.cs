namespace Domain.BankSoal;

public class SoalJawaban
{
    public int Id { get; set; }
    public int SoalId { get; set; }
    public string Jawaban { get; set; }
    public bool IsBenar { get; set; }
    public int Point { get; set; }
}