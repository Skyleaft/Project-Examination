using Shared.RoomSet;
using Shared.Users;

namespace Shared.TakeExam;

public class UserExam
{
    public int Id { get; set; }
    public ApplicationUser? User { get; set; }
    public string UserId { get; set; }
    public bool IsOngoing { get; set; }
    public Room? Room { get; set; }
    public int RoomId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int TimeLeft { get; set; }
    public List<UserAnswer>? UserAnswers { get; set; }
    
    public int? Score
    {
        get
        {
            return Score;
        }
        set
        {
            if (UserAnswers != null)
                Score = UserAnswers.Sum(x => x.Jawaban.Point);
            else Score = 0;
        }
    }

    public double? ScoreNormalize
    {
        get{
            return ScoreNormalize;
        }
        set
        {
            if (UserAnswers != null)
            {
                var score = (double)UserAnswers.Sum(x => x.Jawaban.Point * x.Soal.BobotPoint);
                ScoreNormalize =  score/ (double)Room.Exam.TotalPoint * 100;
            }
                
            else ScoreNormalize = 0;
        }
    }
}