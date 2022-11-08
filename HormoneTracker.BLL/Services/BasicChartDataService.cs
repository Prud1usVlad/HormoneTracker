using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HormoneTracker.BLL.Interfaces;
using HormoneTracker.Core.Models;
using HormoneTracker.Core.Models.ServicesModels;
using HormoneTracker.DAL;

namespace HormoneTracker.BLL.Services
{
    public class BasicChartDataService : IChartDataService
    {
        private readonly HormoneTrackerDBContext _dbContext;

        public BasicChartDataService(HormoneTrackerDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ChartData GetAnalysisData(Analysis analysis)
        {
            var res = new ChartData();

            foreach (var dataItem in analysis.Data)
            {
                res.Data.Add(dataItem.Name,
                    new List<double> { (double)dataItem.NormCoefficient });
            }

            return res;
        }

        public ChartData GetPatientAnalysisData(Patient patient, string analysisName, DateTime? startDate = null)
        {
            var res = new ChartData();

            var date = startDate ?? DateTime.Now.AddDays(-30);
            List<Analysis> analyses = patient.Analyses.Where(x =>
                x.Name == analysisName && x.Date >= date).ToList();

            foreach (var analysis in analyses)
            {
                foreach (var dataItem in analysis.Data)
                {

                    if (!res.Data.ContainsKey(dataItem.Name))
                        res.Data.Add(dataItem.Name, new List<double> { (double)dataItem.NormCoefficient });
                    else
                        res.Data[dataItem.Name].Add((double)dataItem.NormCoefficient);
                }
            }

            return res;
        }
    }
}
