using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nepal.Abstraction.Model;
using Nepal.Abstraction.Service.Business;

namespace Nepal.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MiscController : BaseApiController
    {
        private IMiscService _miscService;
        private readonly ILogger<MiscController> _logger;
        public MiscController(IMiscService miscService, ILogger<MiscController> logger)
        {
            this._miscService = miscService;
            this._logger = logger;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Index()
        {
            try
            {
                var depots = await _miscService.GetDepots();
                var products = await _miscService.GetProducts();

                var miscs = new MiscModel
                {
                    Depots = depots,
                    Products = products
                };
                return Ok(miscs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return CreateApiException(ex);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("Marketer")]
        public async Task<IActionResult> Marketer()
        {
            try
            {
                var UserId = HttpContext.User.Identity.Name;
                var marketer = await _miscService.GetMarketer(UserId);
                return Ok(marketer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return CreateApiException(ex);
            }
        }

    }
}