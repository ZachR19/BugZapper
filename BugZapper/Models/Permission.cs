using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace BugZapper.Models
{
    public class Permission
    {
        public string PermKey { get; set; }

        public TeamModel Team { get; set; }

        public string PermDescription { get; set; }


    }
}
