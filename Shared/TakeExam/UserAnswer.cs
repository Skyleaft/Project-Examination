using Shared.BankSoal;

namespace Shared.TakeExam;

public class UserAnswer
{
    public int Id { get; set; }
    public int UserExamId { get; set; }
    public Soal? Soal { get; set; }
    public int? SoalId { get; set; }
    public SoalJawaban? Jawaban { get; set; }
    public int? SoalJawabanId { get; set; }
}