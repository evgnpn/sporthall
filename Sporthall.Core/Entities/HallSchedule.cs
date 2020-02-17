using System;

namespace Sporthall.Core.Entities
{
    public class HallSchedule : ISchedule
    {
        public HallSchedule()
        {
        }

        public HallSchedule(int hallId, ISchedule schedule)
        {
            HallId = hallId;
            DayOfWeek = schedule.DayOfWeek;
            BeginTime = schedule.BeginTime;
            EndTime = schedule.EndTime;
        }

        public HallSchedule(int hallId, DayOfWeek dayOfWeek, TimeSpan beginTime, TimeSpan endTime)
        {
            HallId = hallId;
            DayOfWeek = dayOfWeek;
            BeginTime = beginTime;
            EndTime = endTime;
        }

        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan BeginTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int HallId { get; set; }
    }
}
