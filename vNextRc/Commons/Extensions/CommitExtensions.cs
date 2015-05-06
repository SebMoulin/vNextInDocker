using System;
using System.Linq;
using Commons.Entities.GitLab;

namespace Commons.Extensions
{
    public static class CommitExtensions
    {
        public static string FirstCommitDate(this GitLabCommit[] commits)
        {
            return commits.ToList().OrderBy(c => c.CreatedAt).First().CreatedAt.ToString("F");
        }
        public static string LastCommitDate(this GitLabCommit[] commits)
        {
            return commits.ToList().OrderBy(c => c.CreatedAt).Last().CreatedAt.ToString("F");
        }
        public static double CommitTimeBetweenFirstAndLastInHours(this GitLabCommit[] commits)
        {
            var orderCommits = commits.ToList().OrderBy(c => c.CreatedAt);

            var first = orderCommits.First().CreatedAt;
            var last = orderCommits.Last().CreatedAt;
            var timeElasped = TimeSpan.FromTicks(last.Ticks) - TimeSpan.FromTicks(first.Ticks);
            return timeElasped.TotalHours;
        }
    }
}