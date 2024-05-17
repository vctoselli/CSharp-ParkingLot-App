using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLot.Domain.Exceptions
{
    public class ParkingLotException : Exception
    {
        public ParkingLotException(string message) : base(message)
        {
        }
    }
}
