using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MovieApp.WebMVC.Models;

namespace MovieApp.WebMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public HomeController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            /*if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                return View(ExhibitionRepository.getExhibitions());
            }*/

            return View(ExhibitionRepository.getExhibitions());
        }
        public IActionResult Details(int id) {
            return View(ExhibitionRepository.getExhibitions().FirstOrDefault(k => k.id == id));
        }
    }
}