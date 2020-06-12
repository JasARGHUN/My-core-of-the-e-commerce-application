using EmployeeStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeStore.Components
{
    public class AppNameViewComponent : ViewComponent
    {
        private readonly IInfoRepository _infoRepository;
        public AppNameViewComponent(IInfoRepository infoRepository)
        {
            _infoRepository = infoRepository;
        }
        public IViewComponentResult Invoke()
        {
            var info = _infoRepository.GetInfo(1);
            return View(info);
        }
    }
}
