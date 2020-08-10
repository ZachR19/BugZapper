using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugZapper.Models
{
    public class TeamModel
    {
        public int ID { get; set; }

        [DisplayName("Name")]
        public string TeamName { get; set; }

        public string Owner_ID { get; set; }

        [DisplayName("Description")]
        public string TeamDescription { get; set; }

        [NotMapped] 
        public ICollection<AppUser> Users { get; set; } = new List<AppUser>();

        public ICollection<ProjectModel> Projects { get; set; } = new List<ProjectModel>();

    }
}
