namespace Domain.BankSoal;

public class Soal
{
    public int Id { get; set; }
    public int ExamId { get; set; }
    public string Pertanyaan { get; set; }
    public List<string> Pilihan { get; set; }
    public string KunciJawaban { get; set; }
    public byte[]? Gambar { get; set; }
    public int Point { get; set; }
}