using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLib.Models
{
    public class FuelPrice
    {
        public int FuelPriceId { get; set; }
        public decimal Price { get; set; }
        public string Date { get; set; }
    }
}
