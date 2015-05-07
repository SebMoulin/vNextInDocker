using System;

namespace TEK.Recruit.Commons.Dtos
{
    public class FinalAssessmentDto
    {
        public bool PassedTheSelection { get; set; }

        public DateTime PassedTheSelectionDate { get; set; }

        public string FinalFeedback { get; set; }
    }
}