using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugZapper.Models
{
    public class TeamModel
    {
        public int ID { get; set; }

        [DisplayName("Name")]
        [Required]
        [MaxLength(50)]
        public string TeamName { get; set; }

        [Required]
        [MaxLength(450)]
        public string Owner_ID { get; set; }

        [DisplayName("Description")]
        [MaxLength(2000)]
        public string TeamDescription { get; set; }

        [NotMapped] 
        public ICollection<AppUser> Users { get; set; } = new List<AppUser>();

        public ICollection<ProjectModel> Projects { get; set; } = new List<ProjectModel>();

    }
}
