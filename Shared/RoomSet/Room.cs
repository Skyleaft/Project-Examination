using Shared.BankSoal;
using Shared.Common;
using Shared.TakeExam;

namespace Shared.RoomSet;

public class Room: IGenericModifier
{
    public int Id { get; set; }
    public string Nama { get; set; }
    public string Kode { get; set; }
    public DateTime JadwalStart { get; set; }
    public DateTime JadwalEnd { get; set; }
    public int Durasi { get; set; }
    public bool IsActive { get; set; }
    public Exam? Exam { get; set; }
    public int? ExamId { get; set; }
    public List<UserExam>? ListPeserta { get; set; }

    public int TotalPeserta
    {
        get
        {
            return TotalPeserta;
        }
        set
        {
            if (ListPeserta != null)
                TotalPeserta = ListPeserta.Count();
        }
    }

    public string? CreatedBy { get; set; }
    public DateTime? CreatedOn { get; set; }
    public string? LastModifiedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
}