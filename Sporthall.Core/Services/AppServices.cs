namespace Sporthall.Core.Services
{
    public class AppServices
    {
        public AppServices(
            IHallService hallService,
            IHallScheduleService hallScheduleService,
            IManagerService managerService,
            ICoachService coachService,
            IUserService userService,
            IAuthorizationService authorizationService,
            ISoloTrainingService soloTrainingService,
            IGroupTrainingService groupTrainingService)
        {
            HallService = hallService;
            HallScheduleService = hallScheduleService;
            ManagerService = managerService;
            CoachService = coachService;
            UserService = userService;
            AuthorizationService = authorizationService;
            SoloTrainingService = soloTrainingService;
            GroupTrainingService = groupTrainingService;
        }

        public IHallService HallService { get; }
        public IHallScheduleService HallScheduleService { get; }
        public IManagerService ManagerService { get; }
        public ICoachService CoachService { get; }
        public IUserService UserService { get; }
        public IAuthorizationService AuthorizationService { get; set; }
        public ISoloTrainingService SoloTrainingService { get; set; }
        public IGroupTrainingService GroupTrainingService { get; set; }
    }
}
