using System.ComponentModel;

namespace WebSite3.Models
{
    public class CandidateEvaluationFormViewModel
    {
        public string ProjectId { get; set; }
        public string CandidateId { get; set; }
        [DisplayName("Code Quality")]
        public int CodeQuality { get; set; }
        [DisplayName("Cultural Fit")]
        public int CulturalFit { get; set; }
        [DisplayName("Technical Interview")]
        public int TechnicalInterview { get; set; }
        [DisplayName("City")]
        public string City { get; set; }
        [DisplayName("Postal Code")]
        public string PostalCode { get; set; }
        [DisplayName("State")]
        public string State { get; set; }
        [DisplayName("Country")]
        public string Country { get; set; }
        [DisplayName("Position")]
        public string Position { get; set; }
        [DisplayName("TEK's Location")]
        public string TekLocation { get; set; }
    }
}