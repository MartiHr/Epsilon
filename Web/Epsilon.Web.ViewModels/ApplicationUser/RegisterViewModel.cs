using System.ComponentModel.DataAnnotations;

namespace Epsilon.Web.ViewModels.ApplicationUser
{
    public class RegisterViewModel
    {
        // TODO: Finish implementing the viewModel: add properties and other things if needed

        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string UserName { get; set; } = null!;

        [Required]
        [EmailAddress]
        [StringLength(60, MinimumLength = 10)]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password and ConfirmPassword do not match")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
