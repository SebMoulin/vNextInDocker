using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TEK.Recruit.Commons.Dtos;

namespace TEK.Recruit.Web.ViewModels
{
    public class CustomerViewModel
    {
        private IEnumerable<CustomerDto> _customers;

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide a name"), DisplayName("Name")]
        public string CustomerName { get; set; }

        public void SetCustomers(IEnumerable<CustomerDto> customers)
        {
            _customers = customers;
        }

        public IEnumerable<CustomerDto> Customers
        {
            get { return _customers; }
            set { _customers = value; }
        }
    }
}
