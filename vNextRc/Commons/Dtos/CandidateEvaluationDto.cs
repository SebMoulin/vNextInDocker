using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commons.Dtos
{
    [Serializable]
    public class CandidateEvaluationDto
    {
        public string ProjectId { get; set; }
        public string CandidateId { get; set; }
        public int CodeQuality { get; set; }
        public int CulturalFit { get; set; }
        public int TechnicalInterview { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Position { get; set; }
        public string TekLocation { get; set; }
    }
}
