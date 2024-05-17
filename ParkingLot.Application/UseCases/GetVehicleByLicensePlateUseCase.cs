using ParkingLot.Communication.Response;
using ParkingLot.Infrastructure;
using ParkingLot.Domain.Services;
using System.Text.RegularExpressions;
using ParkingLot.Domain.Exceptions;

namespace ParkingLot.Application.UseCases
{
    public class GetVehicleByLicensePlateUseCase
    {
        // Initialize new database context
        private readonly ParkingLotDbContext _dbContext;

        private readonly TimeInHoursCalculator? _timeInHoursCalculator;
        public GetVehicleByLicensePlateUseCase(ParkingLotDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _timeInHoursCalculator = new TimeInHoursCalculator();
        }
        public ResponseVehicleInfoCheckedInTimeJson Execute(string licensePlate)
        {
            //Validate(licensePlate);

            // Check if theres a vehicle with the requested license plate in the database
            var entity = _dbContext.VehicleInfo.FirstOrDefault(ve => ve.LicensePlate == licensePlate);
            if (entity is null)
                throw new VehicleNotFoundException($"Vehicle with provided License Plate not found.");

            // Calculate the total time in HOURS a vehicle is parked
            double totalHoursParked = _timeInHoursCalculator.CalculateTimeInHours(entity.CheckInTime);

            return new ResponseVehicleInfoCheckedInTimeJson
            {
                LicensePlate = entity.LicensePlate,
                VehicleModel = entity.VehicleModel,
                VehicleType = entity.VehicleType,
                CheckedIn = entity.CheckedIn,
                CheckInTime = entity.CheckInTime,
                TotalTimeCheckedIn = totalHoursParked,
            };
        }
        /*private static void Validate(string licensePlate)
        {
            if (string.IsNullOrWhiteSpace(licensePlate))
                throw new ErrorOnValidationException($"Invalid License Plate {licensePlate}");

            if (!Regex.IsMatch(licensePlate, @"^[A-Za-z]{3}-\d{4}$") && !Regex.IsMatch(licensePlate, @"^[A-Za-z]{3}\d[A-Za-z]\d{2}$"))
                throw new ErrorOnValidationException($"License plate format is invalid - {licensePlate}. (AAA-1234 or AAA1A11)");
        }*/
    }
}
