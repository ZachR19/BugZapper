using System.ComponentModel.DataAnnotations;

namespace BugZapper.Models
{
    public class TeamPermission
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(450)]
        public string UserID { get; set; }

        [Required]
        [MaxLength(50)]
        public string PermKey { get; set; }

        [MaxLength(100)]
        public string PermDescription { get; set; }

        [Required]
        public int TeamID { get; set; }
    }

    public class ProjectPermission
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(450)]
        public string UserID { get; set; }

        [Required]
        [MaxLength(50)]
        public string PermKey { get; set; }

        [MaxLength(100)]
        public string PermDescription { get; set; }

        [Required]
        public int ProjectID { get; set; }
    }
}
