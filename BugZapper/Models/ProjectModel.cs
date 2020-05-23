using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BugZapper.Models
{
    public class ProjectModel
    {     
        public int ID { get; set; }

        [DisplayName("Project Name")]
        public string ProjName { get; set; }

        public string Description { get; set; }
        
        [DisplayName("Display Name")]
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }

        [DisplayName("Last Edit Name")]
        [DataType(DataType.Date)]
        public DateTime LastEditDate { get; set; }

        [DisplayName("Created By")]
        public string CreatedBy { get; set; }
    }
}
