using AutoMapper;
using ConsoleApp.Parser.Models;
using ConsoleApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Parser.Exceptions;

namespace ConsoleApp.Parser
{
    public class Parser
    {
        static readonly Lazy<Parser> lazy = new Lazy<Parser>(() => new Parser(), true);
        public static Parser Instance => lazy.Value;

        private Parser()
        {
           
        }

        /// <summary>
        /// Parsing fuel responce data
        /// </summary>
        /// <param name="data">received string for parcing</param>
        /// <exception cref="FuelDataParceErrorException">Will be rised in case of responce error</exception>
        /// <returns></returns>
        public async Task<FuelPrice[]> Parse(string data)
        {
            return await Task.Run(() => {
                var parsed = JsonConvert.DeserializeObject<FuelDataResponceModel>(data);

                if (parsed.data != null && !string.IsNullOrEmpty(parsed.data.error))
                {
                    throw new FuelDataParceErrorException(parsed.data.error);
                }

                if (parsed.series.Count() > 0)
                {
                    var first = parsed.series.First();
                    var prices = Mapper.Map<FuelPrice[]>(first.data);

                    return prices;
                }

                return new FuelPrice[] { };

            });

        }
    }
}
