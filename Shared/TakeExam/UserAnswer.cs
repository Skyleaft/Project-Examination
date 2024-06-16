using Shared.BankSoal;

namespace Shared.TakeExam;

public class UserAnswer
{
    public Guid Id { get; set; }
    public Guid UserExamId { get; set; }
    public Soal? Soal { get; set; }
    public Guid? SoalId { get; set; }
    public SoalJawaban? SoalJawaban { get; set; }
    public Guid? SoalJawabanId { get; set; }
}