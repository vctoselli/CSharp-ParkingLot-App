using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ParkingLot.Domain.Exceptions;
using ParkingLot.Communication.Response;
using System.Net;

namespace ParkingLot.API.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var result = context.Exception is ParkingLotException;
            if (result)
            {

            }

            else
            {

            }
        }
        private void HandleProjectException(ExceptionContext context)
        {
            if(context.Exception is VehicleNotFoundException)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                context.Result = new JsonResult(new ResponseErrorJson(context.Exception.Message));
            }

            else if (context.Exception is ErrorOnValidationException)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Result = new JsonResult(new ResponseErrorJson(context.Exception.Message));
            }

            else if (context.Exception is ConflictException)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Conflict;
                context.Result = new JsonResult(new ResponseErrorJson(context.Exception.Message));
            }
        }
        private void ThrownUnknownError(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Result = new JsonResult(new ResponseErrorJson("Unknown Error"));
        }
    }
}
