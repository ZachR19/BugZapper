﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BugZapper.Models
{
    public class TeamModel
    {
        public int ID { get; set; }

        public string TeamName { get; set; }

        public string Owner_ID { get; set; }

        [NotMapped]
        public string Description { get; set; }

        [NotMapped] 
        public ICollection<UserModel> Users { get; set; } = new List<UserModel>();

        public ICollection<ProjectModel> Projects { get; set; } = new List<ProjectModel>();

    }
}
