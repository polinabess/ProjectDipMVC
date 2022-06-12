using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectDipMVC.Models
{
    //private readonly IHttpContextAccessor _httpContextAccessor;

    //public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
    //        IHttpContextAccessor httpContextAccessor
    //        )
    //: base(options)
    //{
    //    _httpContextAccessor = httpContextAccessor;
    //}
    public class ApplicationContext : IdentityDbContext<User>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ApplicationContext(
            DbContextOptions<ApplicationContext> options,
            IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            // Database.EnsureCreated();
        }
    }
}