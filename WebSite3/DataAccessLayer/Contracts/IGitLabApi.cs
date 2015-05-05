using System.Threading.Tasks;
using Commons;
using Commons.Entities.GitLab;
using Framework.Http;

namespace DataAccessLayer.Contracts
{
    public interface IGitLabApi
    {
        Task<string> GetAdminToken();
        Task<string> GetSecurityToken(string email, string password);
        Task<GitLabUser[]> GetUsers(string adminToken);
        Task<GitLabProject[]> GetUserProjects(string userId, string email, string password);
        Task<GitLabProject[]> GetAllProjects(string adminToken);
        Task<GitLabGroup[]> SearchCodingExerciseGroup(string adminToken);
        Task<RestApiResponse<GitLabGroup>> CreateCodingExerciseGroup(string adminToken);
        Task<bool> DeleteProject(string projectid, string adminToken);
        Task<bool> DeleteUser(string candidateid, string adminToken);
        Task<bool> RemoveUserAccess(string projectId, string candidateId, string adminToken);
        Task<RestApiResponse<GitLabUser>> CreateUser(string name, string email, string username, string password, string adminToken);
        Task<RestApiResponse<GitLabProject>> ForkRepository(string email, string password, DevEnv devEnv, string adminToken);
        Task<RestApiResponse<GitLabUser>> SetCandidateAsDevolperOfForkedProject(string projectId, string userId, string adminToken);
        Task<RestApiResponse<GitLabUser>> SetAdminAsOwnerOfForkedProject(string projectId, string adminToken);
        Task<RestApiResponse<GitLabProject>> UpdateProjectVisibilityAndName(string projectId, string username, DevEnv devEnv, string adminToken);
        Task<RestApiResponse<GitLabUser>> AddJenkinsUserToForkedProject(string projectId, string adminToken);
        Task<RestApiResponse<GitLabGroup>> MoveForkedRepoToGroup(string projectId, string groupId, string adminToken);
        Task<GitLabUser[]> GetProjectMembers(string projectId, string adminToken);
        Task<RestApiResponse<GitLabUser>> AddCandidateToGroupMembers(string groupId, string userId, string adminToken);
        Task<GitLabProject> GetProjectById(string projectId, string adminToken);
        Task<GitLabCommit[]> GetProjectCommits(string projectId, string adminToken);
    }
}