using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.ModelBinding;
using TEK.Recruit.Facade.Services;
using TEK.Recruit.Web.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TEK.Recruit.WebApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly ICodingExcerciseEnvironmentFacade _codingExcerciseEnvironmentFacade;

        public AdminController(ICodingExcerciseEnvironmentFacade codingExcerciseEnvironmentFacade)
        {
            if (codingExcerciseEnvironmentFacade == null)
                throw new ArgumentNullException("codingExcerciseEnvironmentFacade");
            _codingExcerciseEnvironmentFacade = codingExcerciseEnvironmentFacade;
        }

        public async Task<ActionResult> Index()
        {
            var vm = await CreateDefaultCustomerViewModel();
            return View(vm);
        }

        public async Task<ActionResult> DeleteRecruiterReportIndex(string customerId)
        {
            var result = await _codingExcerciseEnvironmentFacade.DeleteRecruiterReportIndex(customerId);
            var vm = await CreateDefaultCustomerViewModel();
            return RedirectToAction("Index", vm);
        }

        public async Task<ActionResult> CreateRecruiterReportMapping(string customerId)
        {
            var result = await _codingExcerciseEnvironmentFacade.CreateRecruiterReportMapping(customerId);
            var customers = await _codingExcerciseEnvironmentFacade.GetAllCustomers();
            var vm = await CreateDefaultCustomerViewModel();
            return RedirectToAction("Index", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateCustomer(CustomerViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var customers = await _codingExcerciseEnvironmentFacade.GetAllCustomers();
                viewModel.SetCustomers(customers);
                return View("Index", viewModel);
            }
            var result = await _codingExcerciseEnvironmentFacade.CreateCustomer(viewModel.CustomerName);
            ModelState.Clear();
            var vm = await CreateDefaultCustomerViewModel();
            return View("Index", vm);
        }


        private async Task<CustomerViewModel> CreateDefaultCustomerViewModel()
        {
            var customers = await _codingExcerciseEnvironmentFacade.GetAllCustomers();
            var vm = new CustomerViewModel()
            {
                CustomerName = string.Empty
            };
            vm.SetCustomers(customers);
            return vm;
        }
    }
}