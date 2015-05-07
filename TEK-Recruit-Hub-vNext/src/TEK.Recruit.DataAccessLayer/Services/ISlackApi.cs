using System.Threading.Tasks;

namespace TEK.Recruit.DataAccessLayer.Services
{
    public interface ISlackApi
    {
        Task<bool> SendToSlack(string message);
    }
}
