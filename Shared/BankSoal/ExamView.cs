namespace Shared.BankSoal;

public class ExamView
{
    public int Id { get; set; }
    public string Nama { get; set; }
    public bool IsRandomize { get; set; }
    public List<SoalView>? Soals { get; set; }

    public int TotalSoal
    {
        get { return Soals?.Count ?? 0; }
    }
}