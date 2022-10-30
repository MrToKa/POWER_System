using System.ComponentModel.DataAnnotations;

namespace POWER_System.Models.ApplicationUserModels;

public class RegisterViewModel
{
    [Required]
    [Display(Name = "Username")]
    public string Username { get; set; } = null!;

    [Required]
    [Display(Name = "First name")]
    public string FirstName { get; set; } = null!;

    [Required]
    [Display(Name = "Last name")]
    public string LastName { get; set; } = null!;

    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; } = null!;

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; } = null!;

    [Required]
    [Display(Name = "Office")]

    public string OfficeLocation { get; set; } = null!;

    [Required]
    [Display(Name = "Department")]

    public string Department { get; set; } = null!;

    [Required]
    [Display(Name = "Position")]

    public string Position { get; set; } = null!;
}