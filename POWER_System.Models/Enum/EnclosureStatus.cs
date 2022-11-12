using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace POWER_System.Models.Enum;

public enum EnclosureStatus
{
    [Display(Name = "Design")]
    Design = 1,

    [Display(Name = "Manufacturing")]
    Manufacturing = 2,

    [Display(Name = "InWarehouse")]
    InWarehouse = 3,

    [Display(Name = "Delivered")]
    Delivered = 4,

    [Display(Name = "Mounted")]
    Mounted = 5,
}
