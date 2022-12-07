using POWER_System.Models.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace POWER_System.Models
{
    public class EnclosurePartOrder
    {
        [ForeignKey(nameof(PartOrder))]
        public Guid PartOrderId { get; set; }

        public PartOrder PartOrder { get; set; }

        [ForeignKey(nameof(EnclosurePart))]
        public int EnclosurePartId { get; set; }

        public EnclosurePart EnclosurePart { get; set; }

        public double Quantity { get; set; }

        public OrderDelivery Delivery { get; set; }
    }
}
