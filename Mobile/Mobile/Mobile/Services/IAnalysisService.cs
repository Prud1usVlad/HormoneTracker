using Mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mobile.Services
{
    public interface IAnalysisService
    {
        Task AddAnalysis(Analysis analysis);
        Task RemoveAnalysis(Analysis analysis);
        Task<List<Analysis>> GetAnalysis(string userId);
        Task<List<ChartData>> GetAnalysisChartData(int userId, string name);
        Task<Analysis> GetLastLocalAnalysis(int userId);
    }
}
