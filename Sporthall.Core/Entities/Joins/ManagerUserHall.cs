namespace Sporthall.Core.Entities.Joins
{
    public class ManagerUserHall
    {
        public ManagerUserHall()
        {
        }

        public ManagerUserHall(int hallId, string managerUserId)
        {
            HallId = hallId;
            ManagerUserId = managerUserId;
        }

        public int HallId { get; set; }
        public Hall Hall { get; set; }
        public string ManagerUserId { get; set; }
        public User ManagerUser { get; set; }
    }
}
