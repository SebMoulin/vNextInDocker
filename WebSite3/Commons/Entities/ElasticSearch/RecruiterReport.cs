using Newtonsoft.Json;

namespace Commons.Entities.ElasticSearch
{
    [JsonObject]
    public class RecruiterReport
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "key")]
        public string Key { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "scope")]
        public string Scope { get; set; }
        [JsonProperty(PropertyName = "qualifier")]
        public string Qualifier { get; set; }
        [JsonProperty(PropertyName = "date")]
        public string Date { get; set; }
        [JsonProperty(PropertyName = "creationDate")]
        public string CreationDate { get; set; }
        [JsonProperty(PropertyName = "lname")]
        public string Lname { get; set; }
        [JsonProperty(PropertyName = "version")]
        public string Version { get; set; }
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
        [JsonProperty(PropertyName = "comment_lines_density")]
        public double CommentLinesDensity { get; set; }
        [JsonProperty(PropertyName = "complexity")]
        public double Complexity { get; set; }
        [JsonProperty(PropertyName = "coverage")]
        public double Coverage { get; set; }
        [JsonProperty(PropertyName = "duplicated_blocks")]
        public double DuplicatedBlocks { get; set; }
        [JsonProperty(PropertyName = "ncloc")]
        public double Ncloc { get; set; }
        [JsonProperty(PropertyName = "sqale_index")]
        public double SqaleIndex { get; set; }
        [JsonProperty(PropertyName = "tests")]
        public double Tests { get; set; }

        [JsonProperty(PropertyName = "projectId")]
        public string ProjectId { get; set; }
        [JsonProperty(PropertyName = "projectName")]
        public string ProjectName { get; set; }
        [JsonProperty(PropertyName = "projectWebUrl")]
        public string ProjectWebUrl { get; set; }
        [JsonProperty(PropertyName = "projectCreatedAt")]
        public string ProjectCreatedAt { get; set; }
        [JsonProperty(PropertyName = "projectLastActivityAt")]
        public string ProjectLastActivityAt { get; set; }
        [JsonProperty(PropertyName = "commitsCount")]
        public int CommitsCount { get; set; }
        [JsonProperty(PropertyName = "firstCommitDate")]
        public string FirstCommitDate { get; set; }
        [JsonProperty(PropertyName = "lastCommitDate")]
        public string LastCommitDate { get; set; }
        [JsonProperty(PropertyName = "commitTimeBetweenFirstAndLast")]
        public double CommitTimeBetweenFirstAndLast { get; set; }

        [JsonProperty(PropertyName = "candidateId")]
        public string CandidateId { get; set; }
        [JsonProperty(PropertyName = "codeQuality")]
        public int CodeQuality { get; set; }
        [JsonProperty(PropertyName = "culturalFit")]
        public int CulturalFit { get; set; }
        [JsonProperty(PropertyName = "technicalInterview")]
        public int TechnicalInterview { get; set; }
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
        [JsonProperty(PropertyName = "tekLocation")]
        public string TekLocation { get; set; }
        [JsonProperty(PropertyName = "devEnv")]
        public string DevEnv { get; set; }

        [JsonProperty(PropertyName = "location")]
        public string Location { get; set; }
    }
}
