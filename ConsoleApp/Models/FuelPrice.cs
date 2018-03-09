using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Models
{
    public class FuelPrice
    {
        public decimal Price { get; set; }
        public string Date { get; set; }

        public DateTime ParcedDate
        {
            get
            {
                return DateTime.ParseExact(Date, "yyyyMMdd", CultureInfo.InvariantCulture);
            }
        }
    }
}
