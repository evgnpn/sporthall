namespace Sporthall.WebUI.Helpers
{
    public static class ManagerHelper
    {
        public static string GetManagerTypeString(ManagerType managerType)
        {
            switch (managerType)
            {
                case ManagerType.Manager:
                    return "Менеджер";

                case ManagerType.GeneralManager:
                    return "Главный менеджер";
            }
            return null;
        }

        public static ManagerType? GetManagerEnumByString(string managerType)
        {
            switch (managerType)
            {
                case "Менеджер":
                    return ManagerType.Manager;

                case "Главный менеджер":
                    return ManagerType.GeneralManager;
            }
            return null;
        }
    }
}
