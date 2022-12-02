using Mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mobile.Services
{
    public interface IRecomendationsService
    {
        Task<List<Tip>> LoadTips(int userId);
        Task<List<Recomendation>> LoadRecomendations(int userId);
    }
}
