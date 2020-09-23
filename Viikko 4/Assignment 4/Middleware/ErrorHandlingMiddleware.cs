using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Assignment_4
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorHandlingMiddleware : ControllerBase
    {


        private readonly RequestDelegate _next;

        [Route("error")]
        public NotFoundException Error()
        {

        }


        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {


                return StatusCode(StatusCodes.Status404NotFound);
            }
        }






    }
}
