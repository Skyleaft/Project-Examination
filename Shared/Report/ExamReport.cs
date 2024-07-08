using Shared.Common;

namespace Shared.Report;

public class ExamReport
{
    public string NamaLengkap { get; set; }
    public Gender Gender { get; set; }
    public string? AsalKota { get; set; }
    public string? NomorTelepon { get; set; }
    public int Score { get; set; }
    public int MaxScore { get; set; }
    public double Nilai { get; set; }
}