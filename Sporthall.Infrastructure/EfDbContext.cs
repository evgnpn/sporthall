using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sporthall.Core.Entities;
using Sporthall.Core.Entities.Identity;
using Sporthall.Core.Entities.Joins;

namespace Sporthall.Infrastructure
{
    public class EfDbContext : IdentityDbContext<User, Role, string, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public EfDbContext(DbContextOptions<EfDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder b)
        {
            base.OnModelCreating(b);

            b.Entity<HallSchedule>().HasKey(a => new { a.DayOfWeek, a.HallId });
            b.Entity<Hall>().HasMany<HallSchedule>().WithOne().OnDelete(DeleteBehavior.Cascade);

            b.Entity<ClientUserGroupTraining>().HasKey(a => new { a.ClientUserId, a.GroupTrainingId });
            b.Entity<ClientUserGroupTraining>().HasOne(a => a.GroupTraining).WithMany().HasForeignKey(a => a.GroupTrainingId);
            b.Entity<ClientUserGroupTraining>().HasOne(a => a.ClientUser).WithMany().HasForeignKey(a => a.ClientUserId);

            b.Entity<CoachUserSoloTraining>().HasKey(a => new { a.CoachUserId, a.SoloTrainingId });
            b.Entity<CoachUserSoloTraining>().HasOne(a => a.SoloTraining).WithMany().HasForeignKey(a => a.SoloTrainingId);
            b.Entity<CoachUserSoloTraining>().HasOne(a => a.CoachUser).WithMany().HasForeignKey(a => a.CoachUserId);

            b.Entity<CoachUserGroupTraining>().HasKey(a => new { a.CoachUserId, a.GroupTrainingId });
            b.Entity<CoachUserGroupTraining>().HasOne(a => a.GroupTraining).WithMany().HasForeignKey(a => a.GroupTrainingId);
            b.Entity<CoachUserGroupTraining>().HasOne(a => a.CoachUser).WithMany().HasForeignKey(a => a.CoachUserId);

            b.Entity<CoachUserHall>().HasKey(a => new { a.HallId, a.CoachUserId });
            b.Entity<CoachUserHall>().HasOne(a => a.Hall).WithMany().HasForeignKey(a => a.HallId);
            b.Entity<CoachUserHall>().HasOne(a => a.CoachUser).WithMany().HasForeignKey(a => a.CoachUserId);

            b.Entity<ManagerUserHall>().HasKey(a => new { a.HallId, a.ManagerUserId });
            b.Entity<ManagerUserHall>().HasOne(a => a.Hall).WithMany().HasForeignKey(a => a.HallId);
            b.Entity<ManagerUserHall>().HasOne(a => a.ManagerUser).WithMany().HasForeignKey(a => a.ManagerUserId);

            b.Entity<CoachInfo>().HasKey(a => a.UserId);
            b.Entity<CoachInfo>().HasOne<User>().WithOne().IsRequired().OnDelete(DeleteBehavior.Cascade);

            b.Entity<ManagerInfo>().HasKey(a => a.UserId);
            b.Entity<ManagerInfo>().HasOne<User>().WithOne().IsRequired().OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<Hall> Halls { get; set; }
        public DbSet<SoloTraining> SoloTrainings { get; set; }
        public DbSet<GroupTraining> GroupTrainings { get; set; }
        public DbSet<HallSchedule> HallSchedules { get; set; }
        public DbSet<ClientUserGroupTraining> ClientUserGroupTraining { get; set; }
        public DbSet<CoachUserSoloTraining> CoachUserSoloTraining { get; set; }
        public DbSet<CoachUserGroupTraining> CoachUserGroupTraining { get; set; }
        public DbSet<CoachUserHall> CoachUserHall { get; set; }
        public DbSet<ManagerUserHall> ManagerUserHall { get; set; }
        public DbSet<CoachInfo> CoachInfos { get; set; }
        public DbSet<ManagerInfo> ManagerInfos { get; set; }
    }
}
