using System;

namespace Sporthall.Core.Entities
{
    public interface ISchedule
    {
        DayOfWeek DayOfWeek { get; set; }
        TimeSpan BeginTime { get; set; }
        TimeSpan EndTime { get; set; }
    }
}
