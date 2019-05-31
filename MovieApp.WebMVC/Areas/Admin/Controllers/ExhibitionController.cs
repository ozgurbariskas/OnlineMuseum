using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieApp.WebMVC.Areas.Admin.Models;
using MovieApp.WebMVC.Models;

namespace MovieApp.WebMVC.Areas.Admin.Controllers
{
    public class ExhibitionController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public ExhibitionController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        
        [Authorize]
        public IActionResult Index()
        {
            var isAdmin = Convert.ToBoolean(HttpContext.User.Claims.FirstOrDefault(k => k.Type == ClaimTypes.Role).Value);

            if (!isAdmin) { RedirectToAction("NotAuthorize"); }

            var exhibitions = ExhibitionRepository.getExhibitions();
            return View(exhibitions);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ExhibitionModel model = new ExhibitionModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ExhibitionModel model, IFormFile Picture)
        {
            int thisID = ExhibitionRepository.generateId();
            Exhibition exhibition = new Exhibition
            {
                id = thisID,
                name = model.name,
                price = model.price,
                description = model.description,
                date = model.date,
                image = thisID + ".jpg"
            };
            ExhibitionRepository.AddExhibition(exhibition);

            if (Picture == null || Picture.Length == 0)
                return Content("file not selected");

            var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot\\img",
                        exhibition.image);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await Picture.CopyToAsync(stream);
            }

            return View(model);
        }

        public IActionResult Delete(int id)
        {
            ExhibitionRepository.Delete(id);
            return View();
        }

        public IActionResult NotAuthorize()
        {
            return View();
        }
    }
}