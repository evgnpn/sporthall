using Sporthall.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sporthall.Core.Services
{
    public interface IHallScheduleService
    {
        Task CreateHallScheduleAsync(HallSchedule hallSchedule);

        Task UpdateHallScheduleAsync(HallSchedule hallSchedule);

        Task RemoveHallScheduleAsync(HallSchedule hallSchedule);

        Task<HallSchedule> GetHallScheduleByIdAsync(int hallId, DayOfWeek dayOfWeek);

        Task<IEnumerable<HallSchedule>> GetHallSchedulesByFilterAsync(Expression<Func<HallSchedule, bool>> predicate);
    }
}
