using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Mvc.Rendering;
using TEK.Recruit.Commons.Dtos;
using TEK.Recruit.Web.Utils;

namespace TEK.Recruit.Web.ViewModels
{
    public class NewTestEnvironmentSetUpViewModel
    {
        private CustomerDto[] _customers;
        private readonly List<string> _availableTestEnvs = new List<string>();
        private readonly List<string> _availablePositions = new List<string>();
        private readonly List<string> _availableCenters = new List<string>();

        public NewTestEnvironmentSetUpViewModel()
        {
            _availableTestEnvs.Add("DotNet");
            _availableTestEnvs.Add("Java");
            _availableTestEnvs.Add("iOs");
            _availableTestEnvs.Add("Android");

            _availablePositions.Add("Junior Developer");
            _availablePositions.Add("Intermediate Developer");
            _availablePositions.Add("Senior Developer");
            _availablePositions.Add("Architect");

            _availableCenters.Add("MSC");
            _availableCenters.Add("DSC");
            _availableCenters.Add("ISC");
        }

        public void SetCustomers(CustomerDto[] customers)
        {
            _customers = customers;
        }

        [Required(ErrorMessage = "Email is mandatory to be able to set up the Coding Excercise Environment.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email
        {
            get;
            set;
        }
        [Required(ErrorMessage = "Username is mandatory")]
        public string Username
        {
            get;
            set;
        }
        [Required(ErrorMessage = "Name is mandatory")]
        public string Name
        {
            get;
            set;
        }

        public string Message { get; set; }
        public bool Success { get; set; }

        [DisplayName("Programming language")]
        public IEnumerable<SelectListItem> AvailableTestEnv
        {
            get
            {
                return SelectListItemUtils.CreateList(_availableTestEnvs, p => p, p => p, true);
            }
        }
        [Required(ErrorMessage = "Please select a programming language")]
        public string SelectedDevEnv { get; set; }

        [DisplayName("Position")]
        public IEnumerable<SelectListItem> AvailablePosition
        {
            get
            {
                return SelectListItemUtils.CreateList(_availablePositions, p => p, p => p, true);
            }
        }
        [Required(ErrorMessage = "Please select a position")]
        public string SelectedPosition { get; set; }

        [DisplayName("Customers")]
        public IEnumerable<SelectListItem> Customers
        {
            get
            {
                return SelectListItemUtils.CreateList(_customers, c => c.Name, c => c.Id.ToString(), true);
            }
        }

        [Required(ErrorMessage = "Please select a customer")]
        public string SelectedCustomer { get; set; }


        [DisplayName("City")]
        public string City { get; set; }
        [DisplayName("Postal Code")]
        public string PostalCode { get; set; }
        [DisplayName("State")]
        public string State { get; set; }
        [DisplayName("Country")]
        public string Country { get; set; }


        [DisplayName("TEKSystem's Center")]
        public IEnumerable<SelectListItem> AvailableCenter
        {
            get
            {
                return SelectListItemUtils.CreateList(_availableCenters, p => p, p => p, true);
            }
        }
        [Required(ErrorMessage = "Please select a TEKSystem's Center")]
        public string SelectedCenter { get; set; }

        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [DisplayName("Your email")]
        public string RecruiterEmail { get; set; }
    }
}