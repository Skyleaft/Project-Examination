using System.ComponentModel.DataAnnotations;
using Shared.RoomSet;
using Shared.Users;

namespace Shared.TakeExam;

public class UserExam
{
    public Guid Id { get; set; }
    public ApplicationUser? User { get; set; }
    [MaxLength(100)]
    [Required]
    public string UserId { get; set; }
    public bool IsOngoing { get; set; }
    public bool IsDone { get; set; }
    public Guid RoomId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int TimeLeft { get; set; }
    public List<UserAnswer>? UserAnswers { get; set; }

    public int? Score
    {
        get
        {
            if (UserAnswers != null)
                return UserAnswers.Sum(x => x.SoalJawaban?.Point??0);
            return 0;
        }
    }

    // public double? ScoreNormalize
    // {
    //     get
    //     {
    //         if (UserAnswers != null && Room!=null)
    //         {
    //             var score = (double)UserAnswers.Sum(x => x.SoalJawaban?.Point * x.Soal?.BobotPoint??0);
    //             return score / (double)Room.Exam?.TotalPoint * 100;
    //         }
    //
    //         return 0;
    //     }
    // }
}