using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Nepal.Backend.Controllers
{
    public class BaseApiController : ControllerBase
    {

        [NonAction]
        protected IActionResult CreateApiException(Exception ex)
        {
            return Problem(ex.StackTrace, ex.Source, 500, ex.Message);
        }

    }
}