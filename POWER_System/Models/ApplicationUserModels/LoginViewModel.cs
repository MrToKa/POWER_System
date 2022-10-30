using System.ComponentModel.DataAnnotations;

namespace POWER_System.Models.ApplicationUserModels;

public class LoginViewModel
{
    [Required]
    public string UserName { get; set; } = null!;
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
}