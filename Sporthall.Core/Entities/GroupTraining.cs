using System;

namespace Sporthall.Core.Entities
{
    public class GroupTraining
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan BeginTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool Active { get; set; }
        public int HallId { get; set; }
    }
}
