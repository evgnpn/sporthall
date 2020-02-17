namespace Sporthall.Core.Entities.Joins
{
    public class ClientUserGroupTraining
    {
        public ClientUserGroupTraining()
        {
        }

        public ClientUserGroupTraining(int groupTrainingId, string clientUserId)
        {
            GroupTrainingId = groupTrainingId;
            ClientUserId = clientUserId;
        }

        public int GroupTrainingId { get; set; }
        public GroupTraining GroupTraining { get; set; }
        public string ClientUserId { get; set; }
        public User ClientUser { get; set; }
    }
}
