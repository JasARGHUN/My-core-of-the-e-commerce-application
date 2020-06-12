using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeStore.Models.ViewModels
{
    public class AppDataCreateViewModel
    {
        [Required(ErrorMessage = "Enter URL address")]
        public string UrlAddress { get; set; }
        public List<IFormFile> AppSocialImgs { get; set; } //Application social image.
    }
}
