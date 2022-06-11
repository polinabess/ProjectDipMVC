using System;
using System.Collections.Generic;

namespace ProjectDipMVC.Models
{
    public partial class SectionsProject
    {
        public int SectionsId { get; set; }
        public string? NameSections { get; set; }
        public int? NumberSections { get; set; }
        public int? ProjDscrptId { get; set; }
        public string? NameFileSections { get; set; }
        public byte[]? FileSections { get; set; }

        public virtual ProjectDescript? ProjDscrpt { get; set; }
    }
}
