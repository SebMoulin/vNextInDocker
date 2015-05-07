using System.ComponentModel;

namespace TEK.Recruit.Web.ViewModels
{
    public class AddressViewModel
    {
        [DisplayName("City")]
        public string City { get; set; }

        [DisplayName("Postal Code")]
        public string PostalCode { get; set; }

        [DisplayName("State")]
        public string State { get; set; }

        [DisplayName("Country")]
        public string Country { get; set; }
    }
}
