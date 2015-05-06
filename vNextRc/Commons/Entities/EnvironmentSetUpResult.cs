using System;
using Commons.Entities.GitLab;

namespace Commons.Entities
{
    public class EnvironmentSetUpResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public GitLabUser User { get; set; }
        public string ProjectId { get; set; }
        public string ProjectName { get; set; }
        public Uri HttpUrlToRepo { get; set; }
        public string SshUrlToRepo { get; set; }
        public Uri WebUrl { get; set; }
        public DevEnv DevEnv { get; set; }
        public GitLabGroup Group { get; set; }
    }
}