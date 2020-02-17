namespace Sporthall.Core.Entities.Joins
{
    public class CoachUserHall
    {
        public CoachUserHall()
        {
        }

        public CoachUserHall(int hallId, string coachUserId)
        {
            HallId = hallId;
            CoachUserId = coachUserId;
        }

        public int HallId { get; set; }
        public Hall Hall { get; set; }
        public string CoachUserId { get; set; }
        public User CoachUser { get; set; }
    }
}
