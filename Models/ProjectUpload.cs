using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ProjectDipMVC.Models
{
    public class ProjectUpload : IProjects
    {
        public int ProjectId { get; set; }
        [Display(Name = "Имя проекта")]
        public string Name { get; set; }
        [Display(Name = "Редактор")]
        public int UserId { get; set; }

        [Display(Name = "Файл титула")]
        public IFormFile TitulFile { get; set; }

        [Display(Name = "Редактор")]
        public virtual User User { get; set; } = null!;

        [Display(Name = "Титул")]
        public string? TitulName { get; set; }
    }
}
