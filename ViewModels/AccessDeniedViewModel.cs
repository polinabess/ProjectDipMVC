using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectDipMVC.ViewModels
{
    public class AccessDeniedViewModel
    {
        public AccessDeniedViewModel()
        {

        }
        public string ReturnUrl { get; set; }
    }
}
