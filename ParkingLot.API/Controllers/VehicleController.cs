using Microsoft.AspNetCore.Mvc;
using ParkingLot.Communication.Request;
using ParkingLot.Infrastructure;
using ParkingLot.Application.UseCases;
using ParkingLot.Communication.Response;

namespace ParkingLot.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehicleController : ControllerBase
    {
        // Tirar dúvida: _dbContext está instanciado 2x (Aqui e no método DoVehicleCheckInUseCase.cs). Como resolver?
        private ParkingLotDbContext _dbContext;
        public VehicleController(ParkingLotDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        [HttpGet]
        [Route("GetVehicle/{licensePlate}")]
        [ProducesResponseType(typeof(ResponseVehicleInfoCheckedInTimeJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public IActionResult GetVehicle(string licensePlate)
        {
            var useCase = new GetVehicleByLicensePlateUseCase(_dbContext);

            var response = useCase.Execute(licensePlate);

            return Ok(response);
        }

        [HttpPost]
        [Route("CheckIn")]
        [ProducesResponseType(typeof(ResponseVehicleInfoJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status409Conflict)]
        public IActionResult VehicleCheckIn([FromBody] RequestVehicleCheckInJson request)
        {
            var useCase = new DoVehicleCheckInUseCase(_dbContext);

            var response = useCase.Execute(request);

            return Created(string.Empty, response);
        }

        [HttpPost]
        [Route("CheckOut/{licensePlate}")]
        [ProducesResponseType(typeof(ResponseVehicleInfoCheckOutJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status409Conflict)]
        public IActionResult VehicleCheckOut(string licensePlate)
        {
            var useCase = new DoVehicleCheckOutUseCase(_dbContext);

            var response = useCase.Execute(licensePlate);

            return Ok(response);
        }
    }
}
