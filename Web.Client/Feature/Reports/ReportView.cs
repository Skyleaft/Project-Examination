using Shared.Common;

namespace Web.Client.Feature.Reports;

public class ReportView
{
    public string NamaLengkap { get; set; }
    public Gender Gender { get; set; }
    public string NamaKota { get; set; }
    public string Pekerjaan { get; set; }
    public string PhoneNumber { get; set; }
    public int Score { get; set; }
    public double ScoreNormalize { get; set; }
}