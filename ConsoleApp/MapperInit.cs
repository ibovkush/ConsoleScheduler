using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public static class MapperInit
    {
        public static void Init()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<object[], Models.FuelPrice>()
                    .ForMember(p => p.Date, opts => opts.MapFrom(s => s[0].ToString()))
                    .ForMember(p => p.Price, opts => opts.MapFrom(s => Convert.ToDecimal(s[1])));

                
                config.CreateMap<Models.FuelPrice, DataLib.Models.FuelPrice>()
                    .ForMember(p => p.FuelPriceId, opts => opts.Ignore());

                config.CreateMap<DataLib.Models.FuelPrice, Models.FuelPrice>()
                   .ForMember(p => p.ParcedDate, opts => opts.Ignore());
            });
        }
    }
}
