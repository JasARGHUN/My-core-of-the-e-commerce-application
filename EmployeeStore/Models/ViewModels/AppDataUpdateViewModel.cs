using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeStore.Models.ViewModels
{
    public class AppDataUpdateViewModel : AppDataCreateViewModel
    {
        public int Id { get; set; }
        public string ExistingSocialImagePath { get; set; }
    }
}
