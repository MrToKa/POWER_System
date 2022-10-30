#nullable enable

using POWER_System.Models.Constants;
using POWER_System.Models.Enum;

namespace POWER_System.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Project
    {
        public Project()
        {
            Enclosures = new HashSet<Enclosure>();
            Storages = new HashSet<Storage>();
            SiteServices = new HashSet<SiteService>();
        }

        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets project number. The pattern for name is "P######", where # is a single digit.
        /// </summary>
        [Required]
        [MaxLength(ProjectConstants.NumberMaxLength)]
        [RegularExpression(ProjectConstants.NameRegex)]
        public string Number { get; set; }

        /// <summary>
        /// Gets or sets project name.
        /// </summary>
        [Required]
        [MaxLength(ProjectConstants.NameMaxName)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a short description of the project. It can include base requirements about the design, manufacturing, erection or commissioning of the project.
        /// </summary>
        [MaxLength(ProjectConstants.DescriptionMaxLength)]
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets contractor name.
        /// </summary>
        [Required]
        [MaxLength(ProjectConstants.ContractorMaxLength)]
        public string Contractor { get; set; }

        /// <summary>
        /// Gets or sets the current design phase of the project. It is a preset enumeration and the phase can be - Request, Basic engineering, Detailed engineering, Erection, Commissioning, Finished.
        /// </summary>
        [Required]
        public ProjectStatus Status { get; set; }

        // TODO: Implement documentation class for the project.
        public virtual IEnumerable<Enclosure> Enclosures { get; set; }

        public virtual IEnumerable<Storage> Storages { get; set; }

        public virtual IEnumerable<SiteService> SiteServices { get; set; }
    }
}
