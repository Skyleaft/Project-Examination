using Domain.Common;

namespace Domain.RoomSet;

public class Room: IGenericModifier
{
    public int Id { get; set; }
    public string Nama { get; set; }
    public string Kode { get; set; }
    public DateTime Jadwal { get; set; }
    public bool IsActive { get; set; }
    public Exam? Exam { get; set; }
    public int? ExamId { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? CreatedOn { get; set; }
    public string? LastModifiedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
}