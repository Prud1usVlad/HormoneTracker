using HormoneTracker.BLL.Interfaces;
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
        private readonly IRecomendationService _recomendationsService;

        public RecomendationsController(HormoneTrackerDBContext context,
            IRecomendationService recomendationsService)
        {
            _context = context;
            _recomendationsService = recomendationsService;
        }

        // GET: api/Recomendations/Analysis
        [HttpGet]
        [Route("Patient/{patientId}")]
        public List<Recomendation> GetPatientRecomendations(int patientId)
        {
            Patient patient = _context.Patients.Find(patientId);

            return _recomendationsService.GetRecomendations(patient);
        }

        // GET: api/Recomendations/Analysis
        [HttpGet]
        [Route("Analysis/{analysisId}")]
        public List<Recomendation> GetPatientAnalysisData(int analysisId)
        {
            Analysis analysis = _context.Analyses.Find(analysisId);

            return _recomendationsService.GetRecomendations(analysis);
        }
    }
}
