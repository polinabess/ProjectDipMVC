using System.ComponentModel.DataAnnotations;

namespace ProjectDipMVC.Models
{
    public interface IProjects
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string? TitulName { get; set; }
        public int UserId { get; set; }        
    }
}
