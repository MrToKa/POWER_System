using POWER_System.Models;
using POWER_System.Models.Enum;

namespace POWER_System.Services.Models
{
    public class EnclosureServiceModel
    {
        public EnclosureServiceModel()
        {
            this.Parts = new List<PartServiceModel>();
        }

        public Guid Id { get; set; }

        public string? Plant { get; set; }

        public string? Location { get; set; }

        public string Tag { get; set; } = null!;

        public EnclosureStatus Status { get; set; }

        public string Revision { get; set; } = null!;

        public string? Comment { get; set; }

        public Guid ProjectId { get; set; }

        public virtual IEnumerable<PartServiceModel>? Parts { get; set; }

        //public virtual IEnumerable<CableServiceModel> Cables { get; set; }

        //public virtual IEnumerable<CableOrderServiceModel> CablesOrders { get; set; }
    }
}