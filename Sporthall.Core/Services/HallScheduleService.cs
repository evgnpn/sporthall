using Microsoft.AspNetCore.Identity;
using Sporthall.Core.Entities;
using Sporthall.Core.Identity.Managers;
using Sporthall.Core.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sporthall.Core.Services
{
    public class HallScheduleService : SimpleServiceBase, IHallScheduleService
    {
        public HallScheduleService(AppUserManager userManager, SignInManager<User> signInManager, IUnitOfWork unitOfWork)
            : base(userManager, signInManager, unitOfWork)
        {
        }

        public async Task CreateHallScheduleAsync(HallSchedule hallSchedule)
        {
            UnitOfWork.HallScheduleRepository.Add(hallSchedule);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task RemoveHallScheduleAsync(HallSchedule hallSchedule)
        {
            UnitOfWork.HallScheduleRepository.Remove(hallSchedule);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task UpdateHallScheduleAsync(HallSchedule hallSchedule)
        {
            UnitOfWork.HallScheduleRepository.Update(hallSchedule);
            await UnitOfWork.SaveChangesAsync();
        }

        public Task<HallSchedule> GetHallScheduleByIdAsync(int hallId, DayOfWeek dayOfWeek)
        {
            return UnitOfWork.HallScheduleRepository.GetSingleAsync(a => a.HallId == hallId && a.DayOfWeek == dayOfWeek);
        }

        public Task<IEnumerable<HallSchedule>> GetHallSchedulesByFilterAsync(Expression<Func<HallSchedule, bool>> predicate)
        {
            return UnitOfWork.HallScheduleRepository.GetByFilterAsync(predicate);
        }
    }
}
