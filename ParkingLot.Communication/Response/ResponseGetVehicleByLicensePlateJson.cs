using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLot.Communication.Response
{
    public class ResponseGetVehicleByLicensePlateJson
    {
        public string LicensePlate { get; set; } = string.Empty;
        public string? VehicleModel { get; set; } = string.Empty;
        public string VehicleType { get; set; } = string.Empty;
        public bool CheckedIn { get; set; }
        public DateTime CheckInTime { get; set; }
        public TimeSpan TotalTimeCheckedIn { get; set;}
    }
}
