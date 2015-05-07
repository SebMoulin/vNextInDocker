using System;

namespace TEK.Recruit.Commons.Dtos
{
    public class InterviewEyeballDto
    {
        public bool PassedTheSelection { get; set; }

        public DateTime PassedTheSelectionDate { get; set; }

        public int CodeQuality { get; set; }

        public string Agenda { get; set; }
    }
}