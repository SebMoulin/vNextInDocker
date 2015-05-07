using System.ComponentModel;

namespace TEK.Recruit.Web.ViewModels
{
    public class CandidateProfileViewModel
    {
        public string ProjectId { get; set; }
        public string CandidateId { get; set; }

        [DisplayName("Name")]
        public string Name { get; set; }

        [DisplayName("Username")]
        public string Username { get; set; }

        [DisplayName("Email")]
        public string Email { get; set; }

        [DisplayName("Dev Environment")]
        public string DevEnv { get; set; }

        [DisplayName("Position")]
        public string Position { get; set; }

        [DisplayName("TEKSystem's Center")]
        public string TEKCenter { get; set; }

        public AddressViewModel Address { get; set; }
    }
}
