using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectDipMVC.Models
{
    public partial class Project: IProjects
    {
        public Project()
        {
            ProjectDescripts = new HashSet<ProjectDescript>();
        }

        public int ProjectId { get; set; }
        [Display(Name = "Имя проекта")]
        public string Name { get; set; } = null!;
        [DataType(DataType.DateTime)]
        [Display(Name = "Дата создания")]
        public DateTime DateCreate { get; set; }
        public int UserId { get; set; }
        [Display(Name = "Титул")]
        public string? TitulName { get; set; }
        [ScaffoldColumn(false)]
        public byte[]? TitulFile { get; set; }

        [Display(Name = "Редактор")]
        public virtual User User { get; set; } = null!;
        public virtual ICollection<ProjectDescript> ProjectDescripts { get; set; }
    }
}
