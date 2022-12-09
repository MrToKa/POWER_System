using System.ComponentModel.DataAnnotations.Schema;

namespace POWER_System.Models
{
    public class UserProject
    {
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }


        [ForeignKey(nameof(UserProject))]
        public Guid ProjectId { get; set; }

        public Project Project { get; set; }
    }
}
