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
using Nepal.Business.Service;

namespace Nepal.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MiscController : BaseApiController
    {
        private IMiscService _miscService;
        private readonly ILogger<MiscController> _logger;
        public MiscController(IMiscService miscService, ILogger<MiscController> logger, IKYCClientService kYCClientService)
        {
            this._miscService = miscService;
            this._logger = logger;
            NavMigration.Migrate(kYCClientService, miscService);
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Index()
        {
            try
            {
                var depots = await _miscService.GetDepots();
                var products = await _miscService.GetProducts("OGHARA");

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
        
        [HttpGet]
        [Route("SalePrice/{id}")]
        public async Task<IActionResult> SalePrice(string id)
        {
            try
            {
                var price = await _miscService.GetProducts(id);
                return Ok(price);
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

        [Authorize]
        [HttpGet]
        [Route("Banks")]
        public async Task<IActionResult> Banks()
        {
            try
            {
                var Banks = await _miscService.GetBanks();
                return Ok(Banks.Where(b =>b.BankAccountNo != "").ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return CreateApiException(ex);
            }
        }
    }
}