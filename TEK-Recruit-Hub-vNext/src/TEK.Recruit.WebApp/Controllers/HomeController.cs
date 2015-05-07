using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Mvc;
using TEK.Recruit.Commons.Dtos;
using TEK.Recruit.Facade.Services;
using TEK.Recruit.Framework.Configuration.Services;
using TEK.Recruit.Web.ViewModels;

namespace TEK.Recruit.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICodingExcerciseEnvironmentFacade _codingExcercieEnvironment;
        private readonly IProvideConfig _configProvider;

        public HomeController(ICodingExcerciseEnvironmentFacade codingExcercieEnvironment, IProvideConfig configProvider)
        {
            if (codingExcercieEnvironment == null) throw new ArgumentNullException("codingExcercieEnvironment");
            if (configProvider == null) throw new ArgumentNullException("configProvider");
            _codingExcercieEnvironment = codingExcercieEnvironment;
            _configProvider = configProvider;
        }

        public async Task<ActionResult> Index()
        {
            var customers = await _codingExcercieEnvironment.GetAllCustomers();
            var vm = new NewTestEnvironmentSetUpViewModel();
            vm.SetCustomers(customers);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateTestEnv(NewTestEnvironmentSetUpViewModel viewModel)
        {
            var customers = await _codingExcercieEnvironment.GetAllCustomers();
            viewModel.SetCustomers(customers);
            if (!ModelState.IsValid) return View("Index", viewModel);

            var dto = Mapper.Map<CandidateEnvironmentDto>(viewModel);
            var result = await _codingExcercieEnvironment
                .CreateCodingExcerciseEnvironment(dto);

            ModelState.Clear();
            var json = new
            {
                result.Message,
                result.Success
            };
            return Json(json);
        }

        [HttpPost]
        public JsonResult ValidateEmail(CandidateEnvironmentDto clientJson)
        {
            var adminEmail = _configProvider.GetAdminEmail();
            var jenkinsEmail = _configProvider.GetJenkinsEmail();

            var responseJson = new
            {
                used = false,
                message = string.Empty
            };
            if (clientJson.Email == adminEmail
                || clientJson.Email == jenkinsEmail)
                responseJson = new
                {
                    used = true,
                    message = "This email cannot be used."
                };
            return Json(responseJson);
        }

        public IActionResult Error()
        {
            return View("~/Views/Shared/Error.cshtml");
        }
    }
}
