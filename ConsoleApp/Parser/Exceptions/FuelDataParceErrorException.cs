using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Parser.Exceptions
{
    class FuelDataParceErrorException: Exception
    {
        public FuelDataParceErrorException(string message):base(message)
        { }
    }
}
