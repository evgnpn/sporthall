using System;

namespace Sporthall.WebUI
{
    public interface IDateTimeRange
    {
        DateTime Date { get; set; }
        TimeSpan BeginTime { get; set; }
        TimeSpan EndTime { get; set; }
    }
}
