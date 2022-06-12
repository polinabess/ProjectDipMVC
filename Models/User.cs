using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ProjectDipMVC.Models
{
    public partial class User : IdentityUser
    {
        public User()
        {
            ProjectDescripts = new HashSet<ProjectDescript>();
            Projects = new HashSet<Project>();
        }

        [Key]
        public int UserId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public virtual ICollection<ProjectDescript> ProjectDescripts { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}
