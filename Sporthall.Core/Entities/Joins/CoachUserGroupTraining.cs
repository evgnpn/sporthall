namespace Sporthall.Core.Entities.Joins
{
    public class CoachUserGroupTraining
    {
        public CoachUserGroupTraining()
        {
        }

        public CoachUserGroupTraining(int groupTrainingId, string coachUserId)
        {
            GroupTrainingId = groupTrainingId;
            CoachUserId = coachUserId;
        }

        public int GroupTrainingId { get; set; }
        public GroupTraining GroupTraining { get; set; }
        public string CoachUserId { get; set; }
        public User CoachUser { get; set; }
    }
}
