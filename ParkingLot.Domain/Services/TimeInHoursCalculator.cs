namespace ParkingLot.Domain.Services
{
    public class TimeInHoursCalculator
    {
        public double CalculateTimeInHours(DateTime checkInTime)
        {
            DateTime currentTime = DateTime.UtcNow;

            DateTime checkInTimeUtc = checkInTime.ToUniversalTime();
            TimeSpan totalCheckInTime = currentTime - checkInTimeUtc;

            double totalHoursParked = totalCheckInTime.TotalHours;
            totalHoursParked = Math.Ceiling(totalHoursParked);

            return totalHoursParked;
        }
    }
}
