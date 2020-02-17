namespace Sporthall.Core.Entities.Joins
{
    public class CoachUserSoloTraining
    {
        public CoachUserSoloTraining()
        {
        }

        public CoachUserSoloTraining(int soloTrainingId, string coachUserId)
        {
            SoloTrainingId = soloTrainingId;
            CoachUserId = coachUserId;
        }

        public int SoloTrainingId { get; set; }
        public SoloTraining SoloTraining { get; set; }
        public string CoachUserId { get; set; }
        public User CoachUser { get; set; }
    }
}
