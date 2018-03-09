using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Parser.Models
{
    public class FuelDataResponceModel
    {
        public object request { get; set; }
        public FuelDataErrorModel data { get; set; }
        public IEnumerable<FuelDataSeriesModel> series { get; set; }
    }

    
}
