using HormoneTracker.BLL.Interfaces;
using HormoneTracker.Core.Models;
using HormoneTracker.Core.Models.ServicesModels;
using HormoneTracker.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HormoneTracker.Controllers
{
    [Route("api/Charts")]
    [ApiController]
    public class ChartDataController : ControllerBase
    {
        private readonly HormoneTrackerDBContext _context;
        private readonly IChartDataService _chartService;

        public ChartDataController(HormoneTrackerDBContext context,
            IChartDataService chartService)
        {
            _context = context;
            _chartService = chartService;
        }

        // GET: api/Charts/Analysis
        [HttpGet]
        [Authorize]
        [Route("Analysis/{analysisId}")]
        public ChartData GetAnalysisData(int analysisId)
        {
            Analysis analysis = _context.Analyses.Find(analysisId);

            return _chartService.GetAnalysisData(analysis);
        }

        // GET: api/Charts/Analysis
        [HttpGet]
        [Authorize]
        [Route("Patient/{patientId}/{analysisName}")]
        public ChartData GetPatientAnalysisData(int patientId, string analysisName)
        {
            Patient patient = _context.Patients.Find(patientId);

            return _chartService.GetPatientAnalysisData(patient, analysisName);
        }

    }
}
