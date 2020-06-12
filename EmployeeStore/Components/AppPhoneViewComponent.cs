using EmployeeStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeStore.Components
{
    public class AppPhoneViewComponent : ViewComponent
    {
        private readonly IInfoRepository _infoRepository;
        public AppPhoneViewComponent(IInfoRepository infoRepository)
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
