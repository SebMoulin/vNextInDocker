using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Mvc;
using TEK.Recruit.Commons.Dtos;
using TEK.Recruit.Facade.Services;
using TEK.Recruit.Web.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TEK.Recruit.WebApp.Controllers
{
    public class CodingTestEnvController : Controller
    {
        private readonly ICodingExcerciseEnvironmentFacade _codingExcerciseEnvironmentFacade;

        public CodingTestEnvController(ICodingExcerciseEnvironmentFacade codingExcerciseEnvironmentFacade)
        {
            if (codingExcerciseEnvironmentFacade == null) throw new ArgumentNullException("codingExcerciseEnvironmentFacade");
            _codingExcerciseEnvironmentFacade = codingExcerciseEnvironmentFacade;
        }

        public async Task<ActionResult> Index()
        {
            var projects = await _codingExcerciseEnvironmentFacade.GetAllCandidateEnvironments();
            var model = new CandidateEnvironmentsViewModel()
            {
                CandidateEnvironments = projects
            };
            return View(model);
        }

        public async Task<ActionResult> DeleteTestEnv(string projectid, string candidateid)
        {
            await _codingExcerciseEnvironmentFacade.DeleteTestEnv(projectid, candidateid);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> RemoveUserAccess(string projectid, string candidateid)
        {
            await _codingExcerciseEnvironmentFacade.RemoveUserAccess(projectid, candidateid);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> GenerateReport(string customerId, string projectId, string candidateId)
        {
            await _codingExcerciseEnvironmentFacade.GenerateReport(customerId, projectId, candidateId);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> CandidateEvaluation(string projectId, string candidateId)
        {
            var viewModel = new CandidateEvaluationFormViewModel
            {
                CandidateProfile = new CandidateProfileViewModel()
                {
                    ProjectId = projectId,
                    CandidateId = candidateId
                },
                InterviewFeedback = new InterviewFeedbackViewModel(),
                FinalAssement = new FinalAssessmentViewModel(),
                InterviewEyeball = new InterviewEyeballViewModel()
            };
            var evaluationDto = await _codingExcerciseEnvironmentFacade.GetCandidateInterview(projectId, candidateId);
            if (evaluationDto.CandidateProfile == null &&
                evaluationDto.InterviewEyeball == null &&
                evaluationDto.InterviewFeedback == null &&
                evaluationDto.FinalAssement == null)
                return View(viewModel);

            viewModel = Mapper.Map<CandidateEvaluationFormViewModel>(evaluationDto);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CandidateEvaluation(CandidateEvaluationFormViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            ModelState.Clear();
            var dto = Mapper.Map<InterviewDto>(viewModel);
            await _codingExcerciseEnvironmentFacade.SaveCandidateInterview(dto);
            return RedirectToAction("Index");
        }

    }
}
