using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLot.Domain.Exceptions
{
    public class VehicleNotFoundException : ParkingLotException
    {
        public VehicleNotFoundException(string message) : base(message) { }
    }
}
