using System.ComponentModel.DataAnnotations;

namespace ParkingLot.Domain.Entities
{
    public class VehicleInfo
    {
        [Key]
        public string LicensePlate { get; set; } = string.Empty;
        public string? VehicleModel { get; set;} = string.Empty;
        public string VehicleType { get; set; } = string.Empty;
        public bool CheckedIn { get; set; } = true;
        public DateTime CheckInTime { get; set; } = DateTime.UtcNow;
    }
}
