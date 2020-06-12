using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeStore.Models
{
    public class AppSocialAddress
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter URL address")]
        public string UrlAddress { get; set; }
        public string AppSocialImg { get; set; } //Application social image.
    }
}
