using System;
using System.Collections.Generic;

namespace ProjectDipMVC.Models
{
    public partial class User
    {
        public User()
        {
            ProjectDescripts = new HashSet<ProjectDescript>();
            Projects = new HashSet<Project>();
        }

        public int UserId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public virtual ICollection<ProjectDescript> ProjectDescripts { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}
