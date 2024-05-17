using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLot.Communication.Response
{
    public class ResponseErrorJson
    {
        public string Message { get; set; } = string.Empty;
        public ResponseErrorJson(string message)
        {
            Message = message;
        }
    }
}
