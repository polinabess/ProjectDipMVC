using System;
using System.Collections.Generic;

namespace ProjectDipMVC.Models
{
    public partial class ProjectDescript
    {
        public ProjectDescript()
        {
            SectionsProjects = new HashSet<SectionsProject>();
        }

        public int ProjDscrptId { get; set; }
        public string SectionName { get; set; } = null!;
        public int SectionNumber { get; set; }
        public int UserId { get; set; }
        public int ProjectId { get; set; }

        public virtual Project Project { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<SectionsProject> SectionsProjects { get; set; }
    }
}
