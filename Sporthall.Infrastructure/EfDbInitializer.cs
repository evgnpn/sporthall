using Sporthall.Core;
using Sporthall.Core.Entities;
using Sporthall.Core.Identity.Managers;
using Sporthall.Core.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sporthall.Infrastructure
{
    public class EfDbInitializer
    {
        private readonly AppServices _svcs;
        private readonly EfDbContext _dbContext;
        private readonly AppRoleManager _roleManager;

        public EfDbInitializer(EfDbContext dbContext, AppRoleManager roleManager, AppServices servicesCollection)
        {
            _svcs = servicesCollection;
            _dbContext = dbContext;
            _roleManager = roleManager;
        }

        public async Task CreateDbIfNotExistsAsync()
        {
            if (await _dbContext.Database.EnsureCreatedAsync())
            {
                await CreateRolesAsync();
                await CreateRootUserAsync();
            }
        }

        private async Task CreateRolesAsync()
        {
            var identRes = await _roleManager.CreateAsync(new Role { Name = AppRoles.Coach });

            if (identRes.Succeeded)
            {
                identRes = await _roleManager.CreateAsync(new Role { Name = AppRoles.Manager });
            }

            if (!identRes.Succeeded)
            {
                throw new Exception(identRes.Errors.Select(a => a.Description).Aggregate((a, b) => a + "\n" + b));
            }
        }

        private async Task CreateRootUserAsync()
        {
            var user = new User
            {
                FirstName = "Евгений",
                LastName = "Пинчук",
                UserName = "d1stant",
                Email = "evgnpn@gmail.com",
            };

            await _svcs.UserService.CreateUserAsync(user, "HJ(&CVN(WSM()Vwwsv22234x");
            await _svcs.ManagerService.SetManagerAsync(new ManagerInfo { Description = "ГЛАВНЫЙ МЕНЕДЖЕР!!!", UserId = user.Id });
        }

        public async Task SeedTestDataAsync()
        {
            const int hallsCount = 10;
            const int groupTrainingsCount = 20;
            const int soloTrainingsCount = 30;

            var hallSchedules = new[] {
                new HallSchedule {
                    BeginTime = TimeSpan.Parse("09:00"),
                    EndTime = TimeSpan.Parse("22:00"),
                    DayOfWeek = DayOfWeek.Monday,
                },
               new HallSchedule {
                    BeginTime = TimeSpan.Parse("09:00"),
                    EndTime = TimeSpan.Parse("22:00"),
                    DayOfWeek = DayOfWeek.Tuesday,
                },
                new HallSchedule {
                   BeginTime = TimeSpan.Parse("09:00"),
                    EndTime = TimeSpan.Parse("22:00"),
                    DayOfWeek = DayOfWeek.Wednesday,
                },
                new HallSchedule {
                    BeginTime = TimeSpan.Parse("09:00"),
                    EndTime = TimeSpan.Parse("22:00"),
                    DayOfWeek = DayOfWeek.Thursday,
                },
            };

            for (int i = 0; i < hallsCount; i++)
            {
                await _svcs.HallService.CreateHallAsync(new Hall
                {
                    Description = $"Halleee {i} desc",
                    Address = $"Hall {i} addr",
                    Name = $"Hall {i} name",
                    PhoneNumber = $"Hall {i} phone"
                }, hallSchedules);
            }

            var rand = new Random();

            for (int i = 0; i < groupTrainingsCount; i++)
            {
                await _svcs.GroupTrainingService.CreateGroupTrainingAsync(new GroupTraining
                {
                    Capacity = 2 + i,
                    Active = true,
                    BeginTime = DateTime.Now.TimeOfDay,
                    EndTime = DateTime.Now.TimeOfDay,
                    Name = $"Group training name {i}",
                    Description = $"Scelerisque euismod, iaculis eu lacus nunc mi elit, vehicula ut laoreet ac, aliquam sit amet justo nunc tempor, metus vel placerat suscipit, orci nisl iaculis eros, a tincidunt nisi odio eget lorem nulla condimentum tempor mattis ut vitae feugiat augue.",
                    Date = DateTime.Now.AddDays(rand.Next(1, 22)),
                    HallId = rand.Next(1, hallsCount),
                });
            }

            for (int i = 0; i < soloTrainingsCount; i++)
            {
                await _svcs.SoloTrainingService.CreateSoloTrainingAsync(new SoloTraining
                {
                    Active = true,
                    BeginTime = DateTime.Now.TimeOfDay,
                    EndTime = DateTime.Now.TimeOfDay,
                    Name = $"Solo training name {i}",
                    Description = $"Scelerisque euismod, iaculis eu lacus nunc mi elit, vehicula ut laoreet ac, aliquam sit amet justo nunc tempor, metus vel placerat suscipit, orci nisl iaculis eros, a tincidunt nisi odio eget lorem nulla condimentum tempor mattis ut vitae feugiat augue.",
                    Date = DateTime.Now.AddDays(rand.Next(1, 22)),
                    HallId = rand.Next(1, hallsCount),
                });
            }


        }
    }
}
