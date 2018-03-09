using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLib.Models
{
    public class TestAppContext : DbContext
    {
        public TestAppContext(): base("name=DefaultConnectionString")
        { }

        public DbSet<FuelPrice> FuelPrices { get; set; }
    }
}
