using System.ComponentModel.DataAnnotations;

namespace DatavendingSite.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }

    public class RegisterViewModel
    {
        public RegisterViewModel()
        {
            IsActive = true;
        }

        [Required]
        [RegularExpression("^[A-Za-z0-9]{5,20}$", ErrorMessage = "The {0} must contain at least 5 and not more than 20 alphanumeric characters.")]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        public string Description { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Active?")]
        public bool IsActive { get; set; }

        [Required]
        [Display(Name = "Admin?")]
        public bool IsAdmin { get; set; }

        public ApplicationUser GetUser()
        {
            var user = new ApplicationUser
            {
                UserName = UserName,
                Description = Description,
                IsActive = IsActive,
            };
            return user;
        }
    }

    public class EditUserViewModel
    {
        public EditUserViewModel() { }

        // Allow Initialization with an instance of ApplicationUser:
        public EditUserViewModel(ApplicationUser user)
        {
            Id = user.Id;
            Description = user.Description;
            UserName = user.UserName;
            IsActive = user.IsActive;
        }

        public string Id { get; set; }

        [Required]
        [Display(Name = "User Name")]
        [RegularExpression("^[A-Za-z0-9]{5,20}$", ErrorMessage = "The {0} must contain at least 5 and not more than 20 alphanumeric characters.")]
        public string UserName { get; set; }

        public string Description { get; set; }

        [Required]
        [Display(Name = "Active?")]
        public bool IsActive { get; set; }

        [Required]
        [Display(Name = "Admin?")]
        public bool IsAdmin { get; set; }
    }

    public class ResetPasswordViewModel
    {
        public string Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string UserName { get; set; }
    }
}
