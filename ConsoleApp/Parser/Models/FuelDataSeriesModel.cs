using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Parser.Models
{
    public class FuelDataSeriesModel
    {
        public object[][] data { get; set; }
        public string start { get; set; }
        public string end { get; set; }

    }
}
