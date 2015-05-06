using System;
using System.Threading.Tasks;
using Commons.Dtos;
using Facade.Contracts;
using Microsoft.AspNet.Mvc;
using WebSite3.Models;

namespace vNextRc.Controllers
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
            var projects = await _codingExcerciseEnvironmentFacade.GetAllProjects();
            var model = new CurrentCodingTestEnvironmentViewModel()
            {
                Projects = projects
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

        public async Task<ActionResult> GenerateReport(string projectid, string candidateid)
        {
            await _codingExcerciseEnvironmentFacade.GenerateReport(projectid, candidateid);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> CandidateEvaluation(string projectid, string candidateid)
        {
            var model = new CandidateEvaluationFormViewModel
            {
                ProjectId = projectid,
                CandidateId = candidateid
            };
            var candidateEvaluation = await _codingExcerciseEnvironmentFacade.GetCandidateEvaluation(projectid, candidateid);
            if (candidateEvaluation == null) return View(model);

            model.City = candidateEvaluation.City;
            model.CodeQuality = candidateEvaluation.CodeQuality;
            model.Country = candidateEvaluation.Country;
            model.CulturalFit = candidateEvaluation.CulturalFit;
            model.Position = candidateEvaluation.Position;
            model.PostalCode = candidateEvaluation.PostalCode;
            model.State = candidateEvaluation.State;
            model.TechnicalInterview = candidateEvaluation.TechnicalInterview;
            model.TekLocation = candidateEvaluation.TekLocation;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CandidateEvaluation(CandidateEvaluationFormViewModel evaluationForm)
        {
            if (!ModelState.IsValid) return View(evaluationForm);

            ModelState.Clear();
            var dto = new CandidateEvaluationDto
            {
                ProjectId = evaluationForm.ProjectId,
                CandidateId = evaluationForm.CandidateId,
                City = evaluationForm.City,
                CodeQuality = evaluationForm.CodeQuality,
                Country = evaluationForm.Country,
                CulturalFit = evaluationForm.CulturalFit,
                Position = evaluationForm.Position,
                PostalCode = evaluationForm.PostalCode,
                State = evaluationForm.State,
                TechnicalInterview = evaluationForm.TechnicalInterview,
                TekLocation = evaluationForm.TekLocation
            };
            await _codingExcerciseEnvironmentFacade.SaveCandidateEvaluation(dto);
            return RedirectToAction("Index");
        }

    }
}
