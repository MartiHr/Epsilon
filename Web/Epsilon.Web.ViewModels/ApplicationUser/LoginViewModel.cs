using System.ComponentModel.DataAnnotations;

namespace Epsilon.Web.ViewModels.ApplicationUser
{
    public class LoginViewModel
    {
        // TODO: Finish implementing the viewModel: add properties and other things if needed (for example - email)
        [Required]
        public string UserName { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;   
    }
}
