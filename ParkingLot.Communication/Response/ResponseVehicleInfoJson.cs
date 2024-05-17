namespace ParkingLot.Communication.Response
{
    public class ResponseVehicleInfoJson
    {
        public string LicensePlate { get; set; } = string.Empty;
        public string? VehicleModel { get; set; } = string.Empty;
        public string VehicleType { get; set; } = string.Empty;
        public bool CheckedIn { get; set; }
        public DateTime CheckInTime { get; set; }
        
    }
    public class ResponseVehicleInfoCheckedInTimeJson : ResponseVehicleInfoJson
    {
        public Double TotalTimeCheckedIn { get; set; }
    }

    public class ResponseVehicleInfoCheckOutJson : ResponseVehicleInfoJson
    {
        public Double TotalTimeCheckedIn { get; set; }
        public DateTime CheckOutTime { get; set; }
        public int? FareValue { get; set; }
    }
}
