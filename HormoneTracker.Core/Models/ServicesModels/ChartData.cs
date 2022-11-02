using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HormoneTracker.Core.Models.ServicesModels
{
    public class ChartData
    {
        public string YAxisLabel { get; set; } = "Y Axis";
        public string XAxisLabel { get; set; } = "X Axis";
        public List<string> XAxisLabels { get; set; } = new List<string>();
        public Dictionary<string, List<double>> Data { get; set; } 
            = new Dictionary<string, List<double>>();

    }
}
