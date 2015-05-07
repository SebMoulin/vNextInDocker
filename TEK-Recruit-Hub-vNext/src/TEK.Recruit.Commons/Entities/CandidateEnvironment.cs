using System;

namespace TEK.Recruit.Commons.Entities
{
    public class CandidateEnvironment
    {
        public string CandidateId { get; set; }
        public string Username { get; set; }
        public string CandidateName { get; set; }
        public string Email { get; set; }

        public string CustomerId { get; set; }
        public string CustomerName { get; set; }

        public string DevEnv { get; set; }
        public string Position { get; set; }
        public string TekCenter { get; set; }

        public DateTime? CreatedAt { get; set; }
        public string ProjectId { get; set; }
        public string ProjectName { get; set; }

        public string City { get; set; }
        public string PostalCode { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

        public DateTime? LastActivityAt { get; set; }
    }
}
