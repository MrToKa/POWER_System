using System.ComponentModel.DataAnnotations;

namespace POWER_System.Models.Enum;

public enum ProjectStatus
{
    [Display(Name = "Request")]
    Request = 1,

    [Display(Name = "Basic engineering")]
    BasicEngineering = 2,

    [Display(Name = "Detailed Engineering")]
    DetailedEngineering = 3,

    [Display(Name = "Erection")]
    Erection = 4,

    [Display(Name = "Commissioning")]
    Commissioning = 5,
    
    [Display(Name = "Finished")]
    Finished = 6,
}
