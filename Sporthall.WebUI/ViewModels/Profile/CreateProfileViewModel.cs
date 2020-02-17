using System.ComponentModel.DataAnnotations;

namespace Sporthall.WebUI.ViewModels.Profile
{
    public class CreateProfileViewModel : EditProfileViewModel
    {
        [Required]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password))]
        public string PasswordConfirm { get; set; }

        public bool SignInAfterRegister { get; set; }
    }
}
