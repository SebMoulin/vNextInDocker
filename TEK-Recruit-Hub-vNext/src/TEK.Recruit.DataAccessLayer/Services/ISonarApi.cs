using System.Threading.Tasks;
using TEK.Recruit.Commons.Entities.Sonar;

namespace TEK.Recruit.DataAccessLayer.Services
{
    public interface ISonarApi
    {
        Task<SonarMetrics> GetSonarMetricsForProject(string projectName);
        Task<SonarMetrics[]> GetAllSonarMetrics();
    }
}
