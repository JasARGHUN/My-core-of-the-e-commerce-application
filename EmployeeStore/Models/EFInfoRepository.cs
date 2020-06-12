using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeStore.Models
{
    public class EFInfoRepository : IInfoRepository
    {
        private readonly ApplicationDbContext _context;

        public EFInfoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public AppInfo Add(AppInfo info)
        {
            _context.Add(info);
            _context.SaveChanges();
            return info;
        }

        public AppInfo GetInfo(int id)
        {
            return _context.AppInfos.Find(id);
        }

        public AppInfo Update(AppInfo infoUpdate)
        {
            var data = _context.AppInfos.Attach(infoUpdate);
            data.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return infoUpdate;
        }
    }
}
