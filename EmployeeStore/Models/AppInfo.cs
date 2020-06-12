using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeStore.Models
{
    public class AppInfo
    {
        public int AppInfoID { get; set; }

        [Required(ErrorMessage = "Please, enter Application name")]
        public string AppName { get; set; } //Application name.

        [Required(ErrorMessage = "Please, enter Application address")]
        public string AppAddress { get; set; } //Application address.

        [Required(ErrorMessage = "Please, enter Application first phone")]
        public string AppPhone1 { get; set; } //Application first phone.

        [Required(ErrorMessage = "Please, enter Application second phone")]
        public string AppPhone2 { get; set; } //Application second phone.
        [Required(ErrorMessage = "Please, enter Application description")]
        public string AppDescription { get; set; } //Application second phone.
        [Required(ErrorMessage = "Please enter email address")]
        [Display(Name = "Email Address")]
        [EmailAddress]
        public string AppEmail { get; set; }

        public string AppImage { get; set; } //Application home image.
    }
}
