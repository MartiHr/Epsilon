using System.ComponentModel.DataAnnotations;

using static Epsilon.Data.Common.DataValidation.ApplicationUser;

namespace Epsilon.Web.ViewModels.ApplicationUser
{
    public class RegisterViewModel
    {
        // TODO: Finish implementing the viewModel: add properties and other things if needed

        [Required]
        [StringLength(ApplicationUserUsernameMaxLength, MinimumLength = ApplicationUserUsernameMinLength)]
        public string UserName { get; set; } = null!;

        [Required]
        [EmailAddress]
        [StringLength(ApplicationUserEmailAddressMaxLength, MinimumLength = ApplicationUserEmailAddressMinLength)]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = ApplicationUserConfirmPasswordErrorMessage)]
        public string ConfirmPassword { get; set; } = null!;
    }
}
