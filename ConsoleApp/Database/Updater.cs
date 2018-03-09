using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Database
{
    public class Updater
    {
        static readonly Lazy<Updater> lazy = new Lazy<Updater>(() => new Updater(), true);
        public static Updater Instance => lazy.Value;

        private int getDataForLastNDays;
        private Updater()
        {
            getDataForLastNDays = int.Parse(System.Configuration.ConfigurationManager.AppSettings["GetDataForLastNDays"] ?? "30");
        }


        public async Task<int> Update(IEnumerable<Models.FuelPrice> receivedPrices)
        {

            using (var context = new DataLib.Models.TestAppContext())
            {
                var filtredPrices = receivedPrices.Where(price => price.ParcedDate.AddDays(getDataForLastNDays) > DateTime.Now).ToArray();
                var filtredPricesDates = filtredPrices.Select(x => x.Date).ToArray();
                var existedDates = await context.FuelPrices.Where(dbPrice => filtredPricesDates.Any(date => date == dbPrice.Date)).Select(dbPrice => dbPrice.Date).ToArrayAsync();

                filtredPrices = filtredPrices.Where(rPrice => !existedDates.Any(date => date == rPrice.Date)).ToArray();
                var pricesToAdd = Mapper.Map<DataLib.Models.FuelPrice[]>(filtredPrices);

                context.FuelPrices.AddRange(pricesToAdd);

                await context.SaveChangesAsync();
                return pricesToAdd.Count();
            }
        }
    }
}
