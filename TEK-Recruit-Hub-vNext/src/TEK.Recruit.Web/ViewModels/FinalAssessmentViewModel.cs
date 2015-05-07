using System;
using System.ComponentModel;

namespace TEK.Recruit.Web.ViewModels
{
    public class FinalAssessmentViewModel
    {
        [DisplayName("Passed final selection")]
        public bool PassedFinalSelection { get; set; }

        public DateTime PassedFinalSelectionDate { get; set; }
        [DisplayName("Final Feedback")]
        public string FinalFeedback { get; set; }
    }
}
