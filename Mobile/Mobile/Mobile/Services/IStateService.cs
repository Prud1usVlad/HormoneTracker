using Mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mobile.Services
{
    public interface IStateService
    {
        Task<ChartData> LoadAnalysisChartData(int userId, string name);
        Task<List<Analysis>> LoadAnalysis(int userid);

    }
}
