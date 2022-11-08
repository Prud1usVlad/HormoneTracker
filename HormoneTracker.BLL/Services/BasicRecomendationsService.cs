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
    public class BasicRecomendationsService : IRecomendationService
    {
        private readonly HormoneTrackerDBContext _dbContext;

        public BasicRecomendationsService(HormoneTrackerDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Recomendation> GetRecomendations(Analysis analysis)
        {
            var temp = _dbContext.Data.ToList();
            var res = new List<Recomendation>();

            foreach (var dataItem in analysis.Data)
            {
                var products = _dbContext.Products.Where(x => x.Data.Any(d => d.Name == dataItem.Name));
                products = products.OrderBy(x => x.Data.Where(d => d.Name == dataItem.Name).First().NormCoefficient);

                if (dataItem.NormCoefficient > 200)
                {
                    res.Add(new Recomendation
                    {
                        Header = $"Reduce amount of {dataItem.Name}.",
                        Body = $"In your analysis we found a big amount of {dataItem.Name}." +
                        $" It is {dataItem.NormCoefficient - 100}% higer than norm!" +
                        $" We prepared some products that might help.",
                        Products = products.ToList().Take(3).ToList()
                    });
                }
                else if (dataItem.NormCoefficient < 50)
                {
                    res.Add(new Recomendation
                    {
                        Header = $"Increase amount of {dataItem.Name}.",
                        Body = $"In your analysis we found a small amount of {dataItem.Name}." +
                        $" It is {dataItem.NormCoefficient - 100}% lower than norm!" +
                        $" We prepared some products that might help.",
                        Products = products.ToList().TakeLast(3).ToList()
                    });
                }
            }

            return res;
        }

        public List<Recomendation> GetRecomendations(Patient patient)
        {
            return GetRecomendations(AggregateAnalyses(patient.Analyses));
        }

        private Analysis AggregateAnalyses(IEnumerable<Analysis> analyses)
        {
            var data = new List<Datum>();

            foreach (Analysis analysis in analyses)
            {
                foreach (Datum datum in analysis.Data)
                {
                    var item = data.Find(x => x.Name == datum.Name);
                    if (item != null)
                        item.NormCoefficient = (datum.NormCoefficient + item.NormCoefficient) / 2;
                    else
                        data.Add(new Datum { Name = datum.Name, NormCoefficient = datum.NormCoefficient });

                }
            }

            return new Analysis { Data = data };
        }
    }
}
