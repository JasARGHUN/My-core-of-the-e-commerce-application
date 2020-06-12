using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeStore.Models
{
    public interface IInfoRepository
    {
        AppInfo Add(AppInfo info);
        AppInfo GetInfo(int id);
        AppInfo Update(AppInfo infoUpdate);
    }
}
