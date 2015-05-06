using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Facade.Contracts;
using Microsoft.AspNet.Mvc;
using WebSite3.Models;

namespace vNextRc.Controllers
{
    public class HomeController : Controller
    {

        private readonly ICodingExcerciseEnvironmentFacade _codingExcercieEnvironment;

        public HomeController(ICodingExcerciseEnvironmentFacade codingExcercieEnvironment)
        {
            if (codingExcercieEnvironment == null) throw new ArgumentNullException(nameof(codingExcercieEnvironment));
            _codingExcercieEnvironment = codingExcercieEnvironment;
        }

        public ActionResult Index()
        {
            return View(new NewTestEnvironmentSetUpViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateTestEnv(NewTestEnvironmentSetUpViewModel modelDto)
        {
            if (!ModelState.IsValid) return View("Index", modelDto);

            var result = await _codingExcercieEnvironment
                .CreateNewCodingExcerciseEnvironment(modelDto.Name, modelDto.Email, modelDto.Username, modelDto.SelectedDevEnv);
            ModelState.Clear();
            return View("Index", new NewTestEnvironmentSetUpViewModel()
            {
                Email = string.Empty,
                Username = string.Empty,
                Name = string.Empty,
                Message = result.Message,
                Success = result.Success
            });
        }

        public IActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View("~/Views/Shared/Error.cshtml");
        }
    }
}
