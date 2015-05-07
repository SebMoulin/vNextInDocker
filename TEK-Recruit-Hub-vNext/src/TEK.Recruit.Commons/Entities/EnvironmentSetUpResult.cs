using System;
using TEK.Recruit.Commons.Entities.GitLab;

namespace TEK.Recruit.Commons.Entities
{
    public class EnvironmentSetUpResult
    {
        public string AdminToken { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public GitLabUser User { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string ProjectId { get; set; }
        public string ProjectName { get; set; }
        public Uri HttpUrlToRepo { get; set; }
        public string SshUrlToRepo { get; set; }
        public Uri WebUrl { get; set; }
        public DevEnv DevEnv { get; set; }
        public GitLabGroup Group { get; set; }
    }
}