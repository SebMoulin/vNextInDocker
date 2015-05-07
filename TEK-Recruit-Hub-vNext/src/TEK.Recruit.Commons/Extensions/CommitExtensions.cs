using System;
using System.Linq;
using TEK.Recruit.Commons.Entities.GitLab;

namespace TEK.Recruit.Commons.Extensions
{
    public static class CommitExtensions
    {
        public static DateTime FirstCommitDate(this GitLabCommit[] commits)
        {
            return commits.ToList().OrderBy(c => c.CreatedAt).First().CreatedAt;
        }
        public static DateTime LastCommitDate(this GitLabCommit[] commits)
        {
            return commits.ToList().OrderBy(c => c.CreatedAt).Last().CreatedAt;
        }
        public static double HoursTakenToCompleteCodingExcercise(this GitLabCommit[] commits, DateTime createdAt)
        {
            var last = commits.LastCommitDate();
            var timeElasped = TimeSpan.FromTicks(last.Ticks) - TimeSpan.FromTicks(createdAt.Ticks);
            return timeElasped.TotalHours;
        }
    }
}