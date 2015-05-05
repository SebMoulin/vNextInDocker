using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Mvc.Rendering;
using WebSite3.Utils;

namespace WebSite3.Models
{
    public class NewTestEnvironmentSetUpViewModel
    {
        private readonly List<string> _availableTestEnv = new List<string>();

        public NewTestEnvironmentSetUpViewModel()
        {
            _availableTestEnv.Add("DotNet");
            _availableTestEnv.Add("Java");
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
                return SelectListItemUtils.CreateList(_availableTestEnv, p => p, p => p, true);
            }
        }
        [Required(ErrorMessage = "Please select a programming language")]
        public string SelectedDevEnv { get; set; }
    }
}