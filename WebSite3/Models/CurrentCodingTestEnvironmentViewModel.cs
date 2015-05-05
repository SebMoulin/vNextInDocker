using System.Collections.Generic;
using Commons.Entities.GitLab;

namespace WebSite3.Models
{
    public class CurrentCodingTestEnvironmentViewModel
    {
        public IEnumerable<GitLabProject> Projects { get; set; }
    }
}