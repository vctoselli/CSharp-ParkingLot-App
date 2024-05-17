using ParkingLot.Communication.Request;
using ParkingLot.Communication.Response;
using ParkingLot.Domain.Exceptions;
using ParkingLot.Infrastructure;
using System.Text.RegularExpressions;

namespace ParkingLot.Application.UseCases
{
    public class DoVehicleCheckInUseCase
    {
        private readonly ParkingLotDbContext _dbContext;

        public DoVehicleCheckInUseCase(ParkingLotDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public ResponseVehicleInfoJson Execute(RequestVehicleCheckInJson request)
        {
            Validate(request);

            // Check if the vehicle with the given license plate already exists
            var existingVehicle = _dbContext.VehicleInfo.FirstOrDefault(ve => ve.LicensePlate == NormalizeLicensePlate(request.LicensePlate));

            if (existingVehicle.CheckedIn == true)
            {
                throw new ConflictException($"A vehicle with the License Plate ({existingVehicle.LicensePlate}) is already registered");
            }

            else if (existingVehicle != null)
            {
                // Update the existing record with the new check-in time
                existingVehicle.CheckedIn = true;
                existingVehicle.CheckInTime = DateTime.UtcNow;
                _dbContext.SaveChanges();

                return new ResponseVehicleInfoJson
                {
                    LicensePlate = existingVehicle.LicensePlate,
                    VehicleModel = existingVehicle.VehicleModel,
                    VehicleType = existingVehicle.VehicleType,
                    CheckedIn = existingVehicle.CheckedIn,
                    CheckInTime = existingVehicle.CheckInTime
                };
            }

            else
            {
                // Create a new record for the vehicle
                var entity = new Domain.Entities.VehicleInfo
                {
                    LicensePlate = request.LicensePlate,
                    VehicleModel = request.VehicleModel.ToUpper(),
                    VehicleType = request.VehicleType,
                };

                _dbContext.VehicleInfo.Add(entity);
                _dbContext.SaveChanges();

                return new ResponseVehicleInfoJson
                {
                    LicensePlate = entity.LicensePlate,
                    VehicleModel = entity.VehicleModel,
                    VehicleType = entity.VehicleType,
                    CheckedIn = entity.CheckedIn,
                    CheckInTime = entity.CheckInTime
                };
            }
        }
        private static void Validate(RequestVehicleCheckInJson request)
        {
            if (string.IsNullOrWhiteSpace(request.LicensePlate))
                throw new ErrorOnValidationException($"License plate can't be null or whitespace. {request.LicensePlate}");

            if (!Regex.IsMatch(request.LicensePlate, @"^[A-Za-z]{3}-\d{4}$") && !Regex.IsMatch(request.LicensePlate, @"^[A-Za-z]{3}\d[A-Za-z]\d{2}$"))
                throw new ErrorOnValidationException($"{request.LicensePlate} License Plate format is invalid. License Plate must follow the format \"AAA-1234\" or \"AAA1A11\"");

            if (string.IsNullOrWhiteSpace(request.VehicleType) || (request.VehicleType.ToUpper() != "CAR" && request.VehicleType.ToUpper() != "BIKE"))
                throw new ErrorOnValidationException("Vehicle type must be either 'CAR' or 'BIKE'");
        }
        private static string NormalizeLicensePlate(string licensePlate)
        {
            return licensePlate.ToUpper();
        }
    }
}
