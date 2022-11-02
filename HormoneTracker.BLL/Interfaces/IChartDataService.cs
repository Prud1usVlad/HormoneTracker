using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HormoneTracker.Core.Models;
using HormoneTracker.Core.Models.ServicesModels;

namespace HormoneTracker.BLL.Interfaces
{
    public interface IChartDataService
    {
        ChartData GetPatientAnalysisData(Patient patient, string analysisName, DateTime? startDate = null);
        ChartData GetAnalysisData(Analysis analysis);
    }
}
