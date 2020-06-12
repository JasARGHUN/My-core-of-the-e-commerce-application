using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeStore.Models.ViewModels
{
    public class InfoCreateViewModel
    {
        public List<IFormFile> AppImages { get; set; } //Application image.
    }
}
