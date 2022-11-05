using HormoneTracker.BLL.Services;
using HormoneTracker.Core.Models;
using HormoneTracker.Core.Models.ServicesModels;
using HormoneTracker.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HormoneTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecomendationsController : ControllerBase
    {
        private readonly HormoneTrackerDBContext _context;
        private readonly BasicRecomendationsService _recomendationsService;

        public RecomendationsController(HormoneTrackerDBContext context,
            BasicRecomendationsService recomendationsService)
        {
            _context = context;
            _recomendationsService = recomendationsService;
        }

        // GET: api/Recomendations/Analysis
        [HttpGet]
        [Authorize]
        [Route("Patient/{patientId}")]
        public List<Recomendation> GetPatientRecomendations(int patientId)
        {
            Patient patient = _context.Patients.Find(patientId);

            return _recomendationsService.GetRecomendations(patient);
        }

        // GET: api/Recomendations/Analysis
        [HttpGet]
        [Authorize]
        [Route("Analysis/{analysisId}")]
        public List<Recomendation> GetPatientAnalysisData(int analysisId)
        {
            Analysis analysis = _context.Analyses.Find(analysisId);

            return _recomendationsService.GetRecomendations(analysis);
        }
    }
}
