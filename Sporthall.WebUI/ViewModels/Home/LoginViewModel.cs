using System.ComponentModel.DataAnnotations;

namespace Sporthall.WebUI.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        public bool Remember { get; set; }
    }
}
