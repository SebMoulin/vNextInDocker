using System.Collections.Generic;
using System.Linq;
using TEK.Recruit.Commons.Entities.Sonar;

namespace TEK.Recruit.Commons.Extensions
{
    public static class SonarMrsExtensions
    {

        private static double ResolveMetric(this IEnumerable<SonarMsr> msr, string metricName)
        {
            var metric = msr.FirstOrDefault(m => m.Key == metricName);
            return metric != null ? metric.Val : 0;
        }

        public static double CommentLinesDensity(this SonarMsr[] msr)
        {
            return msr.ResolveMetric("comment_lines_density");
        }
        public static double Complexity(this SonarMsr[] msr)
        {
            return msr.ResolveMetric("complexity");
        }
        public static double Coverage(this SonarMsr[] msr)
        {
            return msr.ResolveMetric("coverage");
        }
        public static double DuplicatedBlocks(this SonarMsr[] msr)
        {
            return msr.ResolveMetric("duplicated_blocks");
        }
        public static double Ncloc(this SonarMsr[] msr)
        {
            return msr.ResolveMetric("ncloc");
        }
        public static double SqaleIndex(this SonarMsr[] msr)
        {
            return msr.ResolveMetric("sqale_index");
        }
        public static double Tests(this SonarMsr[] msr)
        {
            return msr.ResolveMetric("tests");
        }
    }
}