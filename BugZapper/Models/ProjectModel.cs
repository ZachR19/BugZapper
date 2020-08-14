using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugZapper.Models
{
    public class ProjectModel
    {     
        public int ID { get; set; }

        [DisplayName("Project Name")]
        [Required]
        [MaxLength(100)]
        public string ProjName { get; set; }

        [MaxLength(2000)]
        public string Description { get; set; }
        
        [DisplayName("Create Date")]
        [Column(TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        [DisplayName("Last Edit Date")]
        [Column(TypeName = "date")]
        public DateTime LastEditDate { get; set; }
         
        [DisplayName("Created By")]
        [MaxLength(256)]
        public string CreatedBy { get; set; }

        [Required]
        public int TeamID { get; set; }

        public virtual TeamModel Team { get; set; }
    }
}
