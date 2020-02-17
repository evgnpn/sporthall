using Sporthall.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sporthall.Core.Helpers
{
    public static class ScheduleHelper
    {
        public static string GetDayName(DayOfWeek dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case DayOfWeek.Monday: return "Понедельник";
                case DayOfWeek.Tuesday: return "Вторник";
                case DayOfWeek.Wednesday: return "Среда";
                case DayOfWeek.Thursday: return "Четверг";
                case DayOfWeek.Friday: return "Пятница";
                case DayOfWeek.Saturday: return "Суббота";
                case DayOfWeek.Sunday: return "Воскресенье";
                default: return "";
            }
        }

        public static int GetDaySequenceNumber(DayOfWeek dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case DayOfWeek.Monday: return 1;
                case DayOfWeek.Tuesday: return 2;
                case DayOfWeek.Wednesday: return 3;
                case DayOfWeek.Thursday: return 4;
                case DayOfWeek.Friday: return 5;
                case DayOfWeek.Saturday: return 6;
                default: return 7;
            }
        }

        public static IEnumerable<HallSchedule> GetAllScheduleDays()
        {
            return Enum.GetValues(typeof(DayOfWeek)).
                OfType<DayOfWeek>().
                OrderBy(day => GetDaySequenceNumber(day)).
                Select(day => new HallSchedule { DayOfWeek = day }).
                ToList();
        }
    }
}
