using Domain.BankSoal;
using Domain.Common;

namespace Domain.RoomSet;

public class Room: IGenericModifier
{
    public int Id { get; set; }
    public string Nama { get; set; }
    public string Kode { get; set; }
    public DateTime JadwalStart { get; set; }
    public DateTime JadwalEnd { get; set; }
    public int Durasi { get; set; }
    public bool IsActive { get; set; }
    public List<Exam>? ListUjian { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? CreatedOn { get; set; }
    public string? LastModifiedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
}