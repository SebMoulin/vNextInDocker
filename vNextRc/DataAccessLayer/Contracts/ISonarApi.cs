using System.Threading.Tasks;
using Commons.Entities.Sonar;

namespace DataAccessLayer.Contracts
{
    public interface ISonarApi
    {
        Task<SonarMetrics> GetSonarMetricsForProject(string projectName);
        Task<SonarMetrics[]> GetAllSonarMetrics();
    }
}
