namespace ParkingLot.Domain.Services
{
    public class FareCalculator
    {
        public int CalculateFare(double totalHoursParked, string vehicleType)
        {
            int fareValue = 5;
            int payAmount;

            if (totalHoursParked <= 12)
                payAmount = (int)totalHoursParked * fareValue; //Fixed rate of $5 per hour until 12 hours

            else if (totalHoursParked <= 24)
                payAmount = 80; // Flat fee of $80 for 12-24 hours

            else
                payAmount = 300; // After 24 hours, turns into a monthly fee of #300.

            if (vehicleType == "Bike")
                return payAmount /= 2;

            return payAmount;
        }
    }
}
