
using POWER_System.Models.Enum;

namespace POWER_System.Services.Models
{
    public class EnclosureViewModel
    {
        public EnclosureViewModel()
        {
            this.Parts = new List<PartViewModel>();
        }

        public Guid Id { get; set; }

        public string? Plant { get; set; }

        public string? Location { get; set; }

        public string Tag { get; set; } = null!;

        public EnclosureStatus Status { get; set; }

        public string Revision { get; set; } = null!;

        public string? Comment { get; set; }

        public Guid ProjectId { get; set; }

        public virtual List<PartViewModel>? Parts { get; set; }

        //public virtual List<TagQuantityViewModel>? TagsQuantity { get; set; }

        //public virtual IEnumerable<CableServiceModel> Cables { get; set; }

        //public virtual IEnumerable<CableOrderServiceModel> CablesOrders { get; set; }
    }
}
