namespace BugZapper.Models
{
    public class TeamPermission
    {
        public int ID { get; set; }

        public string UserID { get; set; }

        public string PermKey { get; set; }

        public string PermDescription { get; set; }

        public int TeamID { get; set; }
    }

    public class ProjectPermission
    {
        public int ID { get; set; }

        public string UserID { get; set; }

        public string PermKey { get; set; }

        public string PermDescription { get; set; }

        public int ProjectID { get; set; }
    }
}
