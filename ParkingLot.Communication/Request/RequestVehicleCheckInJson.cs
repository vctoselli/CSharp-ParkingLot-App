namespace ParkingLot.Communication.Request
{
    public class RequestVehicleCheckInJson
    {
        public string LicensePlate { get; set; } = string.Empty;
        public string? VehicleModel { get; set; } = string.Empty;
        public string VehicleType { get; set; } = string.Empty;
    }
}
