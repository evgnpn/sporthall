using Sporthall.Core;
using Sporthall.Core.Comparers;
using Sporthall.Core.Comparers.IdComparers;
using Sporthall.Core.Entities;
using Sporthall.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sporthall.WebUI.Extensions.ViewModels
{
    public static class ScheduleExtensions
    {
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
            return Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>().
                OrderBy(day => GetDaySequenceNumber(day)).
                Select(day => new HallSchedule { DayOfWeek = day });
        }

        public static IEnumerable<SelectItem<HallSchedule>> AsAllScheduleSelectors(this IEnumerable<HallSchedule> existHallSchedules)
        {
            IEnumerable<HallSchedule> allSchedules = ScheduleHelper.GetAllScheduleDays();

            if (existHallSchedules == null)
            {
                existHallSchedules = new List<HallSchedule>(allSchedules);
            }

            IEnumerable<HallSchedule> allWithoutExistSchedules = allSchedules.Except(existHallSchedules, new HallScheduleDayComparer());

            IEnumerable<SelectItem<HallSchedule>> allWithoutExistSchedulesSelects = allWithoutExistSchedules.Select(s => new SelectItem<HallSchedule>()
            {
                Model = s,
                Selected = false
            });

            IEnumerable<SelectItem<HallSchedule>> existHallScheduleSelects = existHallSchedules.Select(s => new SelectItem<HallSchedule>()
            {
                Model = s,
                Selected = true
            });

            return allWithoutExistSchedulesSelects.Concat(existHallScheduleSelects).OrderBy(day => GetDaySequenceNumber(day.Model.DayOfWeek));
        }

        public static IEnumerable<HallSchedule> AsAllSchedules(this IEnumerable<HallSchedule> existHallSchedules)
        {
            if (existHallSchedules == null)
            {
                existHallSchedules = new List<HallSchedule>();
            }

            IEnumerable<HallSchedule> allSchedules = ScheduleHelper.GetAllScheduleDays();
            IEnumerable<HallSchedule> allWithoutExistSchedules = allSchedules.Except(existHallSchedules, new HallScheduleIdComparer());

            return allWithoutExistSchedules.Concat(existHallSchedules).OrderBy(day => GetDaySequenceNumber(day.DayOfWeek));
        }

        public static async Task<HallSchedule> RebuildAsync(this HallSchedule hallSchedule, IUnitOfWork unitOfWork)
        {
            return await unitOfWork.HallScheduleRepository
                .GetSingleAsync(a => a.HallId == hallSchedule.HallId && a.DayOfWeek == hallSchedule.DayOfWeek);
        }

        public static async Task<IEnumerable<HallSchedule>> RebuildAsync(this IEnumerable<HallSchedule> hallSchedules, IUnitOfWork unitOfWork)
        {
            List<HallSchedule> hallSch = new List<HallSchedule>();
            foreach (HallSchedule schedule in hallSchedules)
            {
                hallSch.Add(await schedule.RebuildAsync(unitOfWork));
            }

            return hallSch;
        }
    }
}
