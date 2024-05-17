using ParkingLot.Communication.Response;
using ParkingLot.Domain.Exceptions;
using ParkingLot.Domain.Services;
using ParkingLot.Infrastructure;
using System.Text.RegularExpressions;

namespace ParkingLot.Application.UseCases
{
    public class DoVehicleCheckOutUseCase
    {
        private readonly ParkingLotDbContext _dbContext;
        private readonly TimeInHoursCalculator _timeInHoursCalculator;
        private readonly FareCalculator _fareCalculator;

        public DoVehicleCheckOutUseCase(ParkingLotDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _timeInHoursCalculator = new TimeInHoursCalculator();
            _fareCalculator = new FareCalculator();
        }
        public ResponseVehicleInfoJson Execute(string licensePlate)
        {
            // Check if theres a vehicle with the requested license plate in the database
            var entity = _dbContext.VehicleInfo.FirstOrDefault(ve => ve.LicensePlate == licensePlate);

            if (entity is null)
                throw new VehicleNotFoundException($"Vehicle with provided License Plate not found.");

            if (entity.CheckedIn == false)
                throw new ConflictException($"Vehicle with provided License Plate checked-out already.");

            Validate(entity.LicensePlate, entity.CheckInTime);

            // Checks Out vehicle from database 
            entity.CheckedIn = false;
            _dbContext.SaveChanges();

            // Calculate the total time in HOURS a vehicle is parked
            double totalHoursParked = _timeInHoursCalculator.CalculateTimeInHours(entity.CheckInTime);

            // Calculate the fare value base on the total hours the vehicle has been parked
            int fareValue = _fareCalculator.CalculateFare(totalHoursParked, entity.VehicleType);

            return new ResponseVehicleInfoCheckOutJson
            {
                LicensePlate = entity.LicensePlate,
                VehicleModel = entity.VehicleModel,
                VehicleType = entity.VehicleType,
                CheckedIn = entity.CheckedIn,
                CheckInTime = entity.CheckInTime,
                CheckOutTime = DateTime.UtcNow,
                TotalTimeCheckedIn = totalHoursParked,
                FareValue = fareValue
            };
        }
        private void Validate(string licensePlate, DateTime checkInTime)
        {
            /* if (string.IsNullOrWhiteSpace(licensePlate))
                throw new ErrorOnValidationException($"License plate can't be null or whitespace. {licensePlate}");

            if (!Regex.IsMatch(licensePlate, @"^[A-Za-z]{3}-\d{4}$") && !Regex.IsMatch(licensePlate, @"^[A-Za-z]{3}\d[A-Za-z]\d{2}$"))
                throw new ErrorOnValidationException($"{licensePlate} format is invalid. License Plate must follow the format \"AAA-1234\" or \"AAA1A11\""); */

            if (checkInTime > DateTime.UtcNow)
                throw new ErrorOnValidationException("Check-in time can't be greater than Check-Out time");
        }
    }
}
