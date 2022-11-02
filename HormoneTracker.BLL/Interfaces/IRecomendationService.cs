using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HormoneTracker.Core.Models;
using HormoneTracker.Core.Models.ServicesModels;

namespace HormoneTracker.BLL.Interfaces
{
    public interface IRecomendationService
    {
        List<Recomendation> GetRecomendations(Analysis analysis);

        List<Recomendation> GetRecomendations(Patient patient);

    }
}
