using System;
using Newtonsoft.Json;

namespace TEK.Recruit.Commons.Entities.Interview
{
    [JsonObject]
    public class RecruiterReport
    {
        //Sonar info
        [JsonProperty(PropertyName = "commentLinesDensity")]
        public double CommentLinesDensity { get; set; }
        [JsonProperty(PropertyName = "complexity")]
        public double Complexity { get; set; }
        [JsonProperty(PropertyName = "coverage")]
        public double Coverage { get; set; }
        [JsonProperty(PropertyName = "duplicatedBlocks")]
        public double DuplicatedBlocks { get; set; }
        [JsonProperty(PropertyName = "ncloc")]
        public double Ncloc { get; set; }
        [JsonProperty(PropertyName = "sqaleIndex")]
        public double SqaleIndex { get; set; }
        [JsonProperty(PropertyName = "tests")]
        public double Tests { get; set; }

        // Git lab info
        [JsonProperty(PropertyName = "projectId")]
        public string ProjectId { get; set; }
        [JsonProperty(PropertyName = "projectName")]
        public string ProjectName { get; set; }
        [JsonProperty(PropertyName = "projectWebUrl")]
        public string ProjectWebUrl { get; set; }
        [JsonProperty(PropertyName = "projectCreatedAt")]
        public DateTime ProjectCreatedAt { get; set; }
        [JsonProperty(PropertyName = "projectLastActivityAt")]
        public DateTime ProjectLastActivityAt { get; set; }
        [JsonProperty(PropertyName = "commitsCount")]
        public int CommitsCount { get; set; }
        [JsonProperty(PropertyName = "firstCommitDate")]
        public DateTime FirstCommitDate { get; set; }
        [JsonProperty(PropertyName = "lastCommitDate")]
        public DateTime LastCommitDate { get; set; }

        [JsonProperty(PropertyName = "hoursTakenToCompleteCodingExcercise")]
        public double HoursTakenToCompleteCodingExcercise { get; set; }

        // Candidate Profile
        [JsonProperty(PropertyName = "customerId")]
        public string CustomerId { get; set; }
        [JsonProperty(PropertyName = "customerName")]
        public string CustomerName { get; set; }
        [JsonProperty(PropertyName = "candidateId")]
        public string CandidateId { get; set; }
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
        [JsonProperty(PropertyName = "candidateName")]
        public string CandidateName { get; set; }
        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }
        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }
        [JsonProperty(PropertyName = "postalCode")]
        public string PostalCode { get; set; }
        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }
        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }
        [JsonProperty(PropertyName = "position")]
        public string Position { get; set; }
        [JsonProperty(PropertyName = "tekCenter")]
        public string TekCenter { get; set; }
        [JsonProperty(PropertyName = "devEnv")]
        public string DevEnv { get; set; }

        [JsonProperty(PropertyName = "location")]
        public string Location { get; set; }

        //Eyeball
        [JsonProperty(PropertyName = "passedFirstScreening")]
        public bool PassedFirstScreening { get; set; }
        [JsonProperty(PropertyName = "passedFirstScreeningDate")]
        public DateTime PassedFirstScreeningDate { get; set; }
        [JsonProperty(PropertyName = "agenda")]
        public string Agenda { get; set; }
        [JsonProperty(PropertyName = "codeQuality")]
        public int CodeQuality { get; set; }

        //Feedback
        [JsonProperty(PropertyName = "culturalFit")]
        public int CulturalFit { get; set; }
        [JsonProperty(PropertyName = "technicalInterview")]
        public int TechnicalInterview { get; set; }


        //Final Assement
        [JsonProperty(PropertyName = "passedTheSelection")]
        public bool PassedTheSelection { get; set; }
        [JsonProperty(PropertyName = "passedTheSelectionDate")]
        public DateTime PassedTheSelectionDate { get; set; }
        [JsonProperty(PropertyName = "finalFeedback")]
        public string FinalFeedback { get; set; }
    }
}
