using Domain.Common;

namespace Domain.BankSoal;

public class Exam :IGenericModifier
{
    public int Id { get; set; }
    public string Nama { get; set; }
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

    public int TotalPoint
    {
        get { return TotalPoint; }
        private set
        {
            if (Soals != null)
                TotalPoint = Soals.Sum(x => x.Point);
            else TotalPoint = 0;
        }
    }
    
    public byte[] Thumbnail { get; set; }

    public string? CreatedBy { get; set; }
    public DateTime? CreatedOn { get; set; }
    public string? LastModifiedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
}