using EmployeeStore.Models;
using EmployeeStore.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography.X509Certificates;

namespace EmployeeStore.Controllers
{
    [Authorize(Roles = "admin")]
    [Authorize(Policy = "AdminRolePolicy")]
    public class AdminController : Controller
    {
        private readonly IEmployeeRepository _context;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IInfoRepository _infoRepository;
        private readonly IAppDataRepository _appRepository;

        public int pages = 2;

        public AdminController(IEmployeeRepository context, IWebHostEnvironment hostingEnvironment, IInfoRepository infoRepository, IAppDataRepository appRepository)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            _infoRepository = infoRepository;
            _appRepository = appRepository;
        }

        public IActionResult Index(string filter, int page = 1, string sortExpression = "Name")
        {
            var qry = _context.Employees.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                qry = qry.Where(d => d.Name.Contains(filter));
            }

            var model = PagingList.Create(qry, pages, page, sortExpression, "Name");
            model.RouteValue = new RouteValueDictionary { { "filter", filter } };
            model.Action = "Index";
            return View(model);
            //return View(_context.GetAllEmployees().ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = await ProcessUploadFile(model);

                Employee newEmployee = new Employee
                {
                    Name = model.Name,
                    Email = model.Email,
                    Price = model.Price,
                    Department = model.Department,
                    Description = model.Description,
                    PhotoPath = uniqueFileName
                };
                _context.Add(newEmployee);

                TempData["message"] = $"Object {model.Name} was created.";
                return RedirectToAction("Index");
            }
            return View();
        }
        
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Employee emp = await _context.GetEmployee(id);

            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel
            {
                Id = emp.Id,
                Name = emp.Name,
                Email = emp.Email,
                Price = emp.Price,
                Department = emp.Department,
                Description = emp.Description,
                ExistingPhotoPath = emp.PhotoPath
            };

            TempData["message"] = $"Object {employeeEditViewModel.Name} was selected.";
            return View(employeeEditViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeEditViewModel model)
        {

            if (ModelState.IsValid)
            {
                Employee employee = await _context.GetEmployee(model.Id);
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Price = model.Price;
                employee.Department = model.Department;
                employee.Description = model.Description;

                if (model.Photo != null)
                {
                    if (model.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(_hostingEnvironment.WebRootPath,
                            "images", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }

                    employee.PhotoPath = await ProcessUploadFile(model);
                }

                _context.Update(employee);

                TempData["message"] = $"Object {employee.Name} was edited.";

                return RedirectToAction("index");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            Employee model = await _context.GetEmployee(id);

            if (model != null)
            {
                await _context.Delete(model.Id);

                TempData["message"] = $"Object {model.Name} was deleted.";

                return RedirectToAction("Index");
            }

            return View();
        }


        private async Task<string> ProcessUploadFile(EmployeeCreateViewModel model)
        {
            string uniqueFileName = null;

            if (model.Photo != null)
            {
                string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.Photo.CopyToAsync(fileStream);
                }
            }
            return uniqueFileName;
        }

        [HttpPost]
        public IActionResult SeedDatabase()
        {
            SeedDataBase.EnsurePopulated(HttpContext.RequestServices);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult EditInfo() 
        {
            AppInfo info = _infoRepository.GetInfo(1);
            InfoEditViewModel infoEditViewModel = new InfoEditViewModel
            {
                Id = info.AppInfoID,
                AppName = info.AppName,
                AppAddress = info.AppAddress, 
                AppPhone1 = info.AppPhone1,
                AppPhone2 = info.AppPhone2, 
                AppDescription = info.AppDescription,
                AppEmail = info.AppEmail, 
                ExistingImagePath = info.AppImage,
            };
            return View(infoEditViewModel);
        }
        [HttpPost]
        public IActionResult EditInfo(InfoEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppInfo info = _infoRepository.GetInfo(model.Id = 1);
                info.AppName = model.AppName;
                info.AppAddress = model.AppAddress;
                info.AppPhone1 = model.AppPhone1;
                info.AppPhone2 = model.AppPhone2;
                info.AppDescription = model.AppDescription;
                info.AppEmail = model.AppEmail;

                if (model.AppImages != null) 
                {
                    if (model.ExistingImagePath != null)
                    {
                        string filePath = Path.Combine(_hostingEnvironment.WebRootPath,
                            "images", model.ExistingImagePath);
                        System.IO.File.Delete(filePath);
                    }
                    info.AppImage = ProcessUploadAppImage(model);
                }

                _infoRepository.Update(info);
                TempData["message"] = $"Application {model.AppName} data has been updated..";
                return RedirectToAction("Index");
            }
            return View();
        }

        //Application Social URL-address

        [HttpGet]
        public IActionResult CreateSocialData()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateSocialData(AppDataCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueSocialFileName = ProcessUploadAppSocialImage(model);

                AppSocialAddress newAppSocialAddress = new AppSocialAddress
                {
                    UrlAddress = model.UrlAddress,
                    AppSocialImg = uniqueSocialFileName
                };

                _appRepository.Add(newAppSocialAddress);

                TempData["message"] = $"Address {model.UrlAddress} was created.";
                return RedirectToAction("SocialList");
            }
            return View();
        }

        [HttpGet]
        public IActionResult EditSocialData(int id)
        {
            AppSocialAddress data = _appRepository.GetInfo(id);

            AppDataUpdateViewModel item = new AppDataUpdateViewModel
            {
                Id = data.Id,
                UrlAddress = data.UrlAddress,
                ExistingSocialImagePath = data.AppSocialImg
            };
            TempData["message"] = $"Object {item.UrlAddress} was selected.";
            return View(item);
        }

        [HttpPost]
        public IActionResult EditSocialData(AppDataUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppSocialAddress item = _appRepository.GetInfo(model.Id);
                item.UrlAddress = model.UrlAddress;

                if (model.AppSocialImgs != null) 
                {
                    if (model.ExistingSocialImagePath != null)
                    {
                        string filePath = Path.Combine(_hostingEnvironment.WebRootPath,
                            "images", model.ExistingSocialImagePath);
                        System.IO.File.Delete(filePath);
                    }
                    item.AppSocialImg = ProcessUploadAppSocialImage(model);
                }

                _appRepository.Update(item);

                TempData["message"] = $"Object {item.UrlAddress} was edited.";

                return RedirectToAction("SocialList");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSocialAddress(int id)
        {
            AppSocialAddress deletePerson = await _appRepository.Delete(id);
            if (deletePerson != null)
            {
                TempData["message"] = $"Url address: {deletePerson.UrlAddress} was deleted";
            }
            return RedirectToAction("SocialList");
        }

        public IActionResult SocialList()
        {
            var obj = _appRepository.AppSocialAddress;
            return View(obj);
        }

        private string ProcessUploadAppImage(InfoCreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.AppImages != null && model.AppImages.Count > 0)
            {
                foreach (IFormFile photo in model.AppImages)
                {
                    var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        photo.CopyTo(fileStream);
                    }
                }
            }

            return uniqueFileName;
        }
        private string ProcessUploadAppSocialImage(AppDataCreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.AppSocialImgs != null && model.AppSocialImgs.Count > 0)
            {
                foreach (IFormFile photo in model.AppSocialImgs)
                {
                    var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        photo.CopyTo(fileStream);
                    }
                }
            }

            return uniqueFileName;
        }
    }
}
