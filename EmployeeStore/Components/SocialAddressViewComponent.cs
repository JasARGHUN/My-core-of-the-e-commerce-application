using EmployeeStore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace EmployeeStore.Components
{
    public class SocialAddressViewComponent : ViewComponent
    {
        private IAppDataRepository _context;

        public SocialAddressViewComponent(IAppDataRepository context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            return View(_context.GetAllAppSocialAddress());
        }
    }
}
