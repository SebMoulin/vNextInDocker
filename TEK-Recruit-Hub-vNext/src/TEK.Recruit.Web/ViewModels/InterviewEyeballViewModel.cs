using System;
using System.ComponentModel;

namespace TEK.Recruit.Web.ViewModels
{
    public class InterviewEyeballViewModel
    {
        [DisplayName("Passed first screenning selection")]
        public bool PassedFirstScreenSelection { get; set; }

        public DateTime PassedFirstScreenSelectionDate { get; set; }

        [DisplayName("Code Quality")]
        public int CodeQuality { get; set; }
        [DisplayName("Agenda")]
        public string Agenda { get; set; }
    }
}
