using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BugZapper.Models
{
    public class TeamModel
    {
        [NotMapped]
        public uint AdminId { get; set; }
        public int ID { get; set; }

        public string TeamName { get; set; }


        [NotMapped]
        public List<UserModel> Users { get; set; }

        [NotMapped]
        public List<ProjectModel> Projects { get; set; }

    }
}
